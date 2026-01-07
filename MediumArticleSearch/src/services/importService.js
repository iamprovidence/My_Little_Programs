import JSZip from 'jszip';

/**
 * Parses Medium export ZIP file and extracts articles
 * @param {File} file - ZIP file from Medium export
 * @returns {Promise<Array>} Array of articles
 */
export async function parseMediumExport(file) {
  console.log('Starting to parse Medium export file...');
  console.log('File name:', file.name);
  console.log('File size:', file.size);

  try {
    // Load the ZIP file
    const zip = await JSZip.loadAsync(file);
    console.log('ZIP file loaded successfully');

    const articles = [];

    // Find the posts directory
    const postsFolder = zip.folder('posts');

    if (!postsFolder) {
      console.log('No posts folder found, checking root...');
      // Sometimes files are in root
      const files = Object.keys(zip.files);
      console.log('Files in ZIP:', files);
    }

    // Iterate through all HTML files
    for (const [filename, zipEntry] of Object.entries(zip.files)) {
      // Skip directories and non-HTML files
      if (zipEntry.dir || !filename.endsWith('.html')) {
        continue;
      }

      // Skip non-article files
      if (
        filename.includes('index.html') ||
        filename.includes('profile.html') ||
        filename.includes('settings.html') ||
        filename.includes('security.html') ||
        filename.includes('stats.html') ||
        filename.includes('bookmarks') ||
        filename.includes('highlights') ||
        filename.includes('lists') ||
        filename.includes('publications') ||
        filename.includes('responses') ||
        filename.includes('comments') ||
        filename.includes('claps') ||
        filename.includes('followers') ||
        filename.includes('following') ||
        filename.includes('drafts') ||
        filename.includes('draft')
      ) {
        console.log('Skipping non-article file:', filename);
        continue;
      }

      // Only process files in posts folder, but NOT in responses/comments/drafts subfolders
      if (!filename.includes('posts/') && !filename.includes('posts\\')) {
        console.log('Skipping non-post file:', filename);
        continue;
      }

      // Skip if it's a response/comment/draft (these are typically in a subfolder or have specific naming)
      const filenameLower = filename.toLowerCase();
      if (filenameLower.includes('response') ||
          filenameLower.includes('comment') ||
          filenameLower.includes('reply') ||
          filenameLower.includes('draft')) {
        console.log('Skipping comment/response/draft file:', filename);
        continue;
      }

      console.log('Processing article file:', filename);

      try {
        const content = await zipEntry.async('text');
        const article = parseArticleHTML(content, filename);

        if (article && article.title && article.content) {
          articles.push(article);
          console.log('Parsed article:', article.title);
        }
      } catch (err) {
        console.error(`Error parsing ${filename}:`, err);
      }
    }

    console.log(`Successfully parsed ${articles.length} articles from export`);

    // Sort articles by publish date, descending (newest first)
    articles.sort((a, b) => {
      const dateA = new Date(a.pubDate);
      const dateB = new Date(b.pubDate);
      return dateB - dateA; // Descending order
    });

    return articles;
  } catch (error) {
    console.error('Error parsing ZIP file:', error);
    throw new Error('Failed to parse Medium export file. Please ensure it is a valid ZIP file from Medium.');
  }
}

/**
 * Parses an individual article HTML file
 * @param {string} html - HTML content
 * @param {string} filename - Original filename
 * @returns {Object} Article object
 */
function parseArticleHTML(html, filename) {
  const parser = new DOMParser();
  const doc = parser.parseFromString(html, 'text/html');

  // Extract title - look for article title specifically
  let title = doc.querySelector('h1.p-name')?.textContent?.trim() ||
              doc.querySelector('article h1')?.textContent?.trim() ||
              doc.querySelector('h1')?.textContent?.trim() ||
              doc.querySelector('title')?.textContent?.trim() ||
              filename.replace('.html', '').replace(/^.*[\\\/]/, '');

  // Skip if title looks like metadata
  if (!title || title.toLowerCase().includes('profile') ||
      title.toLowerCase().includes('settings') ||
      title.toLowerCase().includes('security')) {
    return null;
  }

  // Extract content - focus on article body
  const articleElement = doc.querySelector('article') || doc.querySelector('.postArticle-content') || doc.body;

  // Get paragraphs and headings from article content
  const paragraphs = Array.from(articleElement.querySelectorAll('p, h2, h3, h4, h5, h6, li'));

  // Filter out metadata paragraphs (like IP addresses, dates without context)
  const contentParagraphs = paragraphs
    .map(p => p.textContent?.trim())
    .filter(text => {
      if (!text || text.length < 10) return false;
      // Skip if it looks like an IP address or metadata
      if (/^\d+\.\d+\.\d+\.\d+$/.test(text)) return false;
      if (/^(January|February|March|April|May|June|July|August|September|October|November|December)\s+\d+,\s+\d{4}$/.test(text)) return false;
      return true;
    });

  const content = contentParagraphs.join(' ');

  // Validate we have actual article content (not comments which are usually shorter)
  if (!content || content.length < 1000) {
    console.log('Skipping - content too short (likely a comment):', content.length, 'chars');
    return null;
  }

  // Check if content looks like an article (should have multiple sentences)
  const sentenceCount = (content.match(/[.!?]+/g) || []).length;
  if (sentenceCount < 10) {
    console.log('Skipping - not enough sentences (likely a comment):', sentenceCount);
    return null;
  }

  // Check word count - articles typically have 200+ words, comments are shorter
  const wordCount = content.split(/\s+/).filter(word => word.length > 0).length;
  if (wordCount < 200) {
    console.log('Skipping - not enough words (likely a comment):', wordCount, 'words');
    return null;
  }

  // Check if title looks like a comment response pattern
  const titleLower = title.toLowerCase();
  if (titleLower.includes('re:') ||
      titleLower.includes('response to') ||
      titleLower.startsWith('reply') ||
      titleLower.length < 10) {
    console.log('Skipping - title looks like a comment:', title);
    return null;
  }

  // Extract date if available
  const timeElement = doc.querySelector('time');
  const pubDate = timeElement?.getAttribute('datetime') ||
                  timeElement?.textContent ||
                  new Date().toISOString();

  // Extract tags/categories
  const categories = [];
  const tagElements = doc.querySelectorAll('a[rel="tag"], .tag, [data-tag]');
  tagElements.forEach(tag => {
    const tagText = tag.textContent?.trim();
    if (tagText && tagText.length > 1 && tagText.length < 50) {
      categories.push(tagText);
    }
  });

  // Extract link if available - try multiple methods
  let link = '';

  // Method 1: Look for anchor with class "p-canonical" (most reliable for Medium exports)
  const pCanonicalLink = doc.querySelector('a.p-canonical');
  if (pCanonicalLink) {
    link = pCanonicalLink.getAttribute('href') || '';
    console.log('Found p-canonical link:', link);
  }

  // Method 2: Canonical link tag in head
  if (!link) {
    const canonicalLink = doc.querySelector('link[rel="canonical"]');
    if (canonicalLink) {
      link = canonicalLink.getAttribute('href') || '';
    }
  }

  // Method 3: Look for Medium links in the document
  if (!link) {
    const links = Array.from(doc.querySelectorAll('a[href*="medium.com"]'));
    const mediumLink = links.find(a => {
      const href = a.getAttribute('href');
      return href && (href.includes('/@') || href.includes('/p/'));
    });
    if (mediumLink) {
      link = mediumLink.getAttribute('href') || '';
    }
  }

  // Method 4: Look for meta property og:url
  if (!link) {
    const ogUrl = doc.querySelector('meta[property="og:url"]');
    if (ogUrl) {
      link = ogUrl.getAttribute('content') || '';
    }
  }

  // Description is usually first paragraph
  const description = contentParagraphs[0]?.substring(0, 200) || '';

  return {
    title,
    link,
    pubDate,
    author: '',
    description,
    content,
    categories
  };
}
