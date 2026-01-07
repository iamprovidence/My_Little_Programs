/**
 * Service to fetch and search Medium articles
 * Uses Medium's RSS feed for article retrieval
 */

/**
 * Parse RSS XML to extract articles
 */
function parseRSSFeed(xmlText) {
  const parser = new DOMParser();
  const xmlDoc = parser.parseFromString(xmlText, 'text/xml');

  const items = xmlDoc.querySelectorAll('item');
  const articles = [];

  items.forEach(item => {
    const title = item.querySelector('title')?.textContent || '';
    const link = item.querySelector('link')?.textContent || '';
    const pubDate = item.querySelector('pubDate')?.textContent || '';
    const creator = item.querySelector('creator')?.textContent ||
                   item.querySelector('author')?.textContent || '';
    const description = item.querySelector('description')?.textContent || '';
    const content = item.querySelector('encoded')?.textContent ||
                   item.querySelector('content')?.textContent ||
                   description;

    // Get categories
    const categoryElements = item.querySelectorAll('category');
    const categories = Array.from(categoryElements).map(cat => cat.textContent);

    articles.push({
      title,
      link,
      pubDate,
      author: creator,
      description,
      content,
      categories
    });
  });

  return articles;
}

/**
 * Fetches articles from Medium RSS feed
 * @param {string} username - Medium username (e.g., "@yourusername")
 * @returns {Promise<Array>} Array of articles
 */
export async function fetchMediumArticles(username) {
  try {
    // Remove @ if present
    const cleanUsername = username.replace('@', '').trim();

    if (!cleanUsername) {
      throw new Error('Please enter a valid Medium username');
    }

    console.log('Fetching articles for username:', cleanUsername);
    console.log('Using RSS feed (limited to 10 articles)...');

    const rssUrl = `https://medium.com/feed/@${cleanUsername}`;
    console.log('RSS URL:', rssUrl);

    // Try method 1: Direct RSS parsing via AllOrigins (gets all articles from RSS)
    try {
      console.log('Method 1: Trying AllOrigins proxy to fetch raw RSS...');
      const allOriginsUrl = `https://api.allorigins.win/raw?url=${encodeURIComponent(rssUrl)}`;
      const response = await fetch(allOriginsUrl);

      console.log('AllOrigins response status:', response.status);

      if (response.ok) {
        const xmlText = await response.text();
        console.log('RSS XML length:', xmlText.length);
        console.log('RSS XML preview:', xmlText.substring(0, 500));

        const articles = parseRSSFeed(xmlText);
        console.log(`Parsed ${articles.length} articles from RSS feed`);

        if (articles.length > 0) {
          console.log(`Success! Fetched ${articles.length} articles via AllOrigins`);
          console.log('First article:', articles[0]);
          console.log('Last article:', articles[articles.length - 1]);
          return articles;
        }
      }
    } catch (err) {
      console.log('AllOrigins method failed:', err.message);
      console.error('Full error:', err);
    }

    // Try method 1b: Different CORS proxy - cors-anywhere alternative
    try {
      console.log('Method 1b: Trying corsproxy.io...');
      const corsProxyUrl = `https://corsproxy.io/?${encodeURIComponent(rssUrl)}`;
      const response = await fetch(corsProxyUrl);

      console.log('corsproxy.io response status:', response.status);

      if (response.ok) {
        const xmlText = await response.text();
        console.log('RSS XML length:', xmlText.length);

        const articles = parseRSSFeed(xmlText);
        console.log(`Parsed ${articles.length} articles from RSS feed`);

        if (articles.length > 0) {
          console.log(`Success! Fetched ${articles.length} articles via corsproxy.io`);
          return articles;
        }
      }
    } catch (err) {
      console.log('corsproxy.io method failed:', err.message);
    }

    // Try method 2: rss2json with default count (fallback - only 10 articles)
    console.log('Method 2: Falling back to rss2json with default count...');
    const proxyUrl = `https://api.rss2json.com/v1/api.json?rss_url=${encodeURIComponent(rssUrl)}`;
    const response = await fetch(proxyUrl);

    if (!response.ok) {
      throw new Error('Failed to fetch articles. Please verify the username exists on Medium.');
    }

    const data = await response.json();

    if (data.status !== 'ok' || !data.items || data.items.length === 0) {
      throw new Error('No articles found for this user.');
    }

    console.log(`Fetched ${data.items.length} articles via rss2json (limited to 10)`);

    return data.items.map(item => ({
      title: item.title,
      link: item.link,
      pubDate: item.pubDate,
      author: item.author,
      description: item.description,
      content: item.content,
      categories: item.categories || []
    }));
  } catch (error) {
    console.error('Error fetching Medium articles:', error);
    throw error;
  }
}

/**
 * Searches articles for a specific term
 * @param {Array} articles - Array of articles to search
 * @param {string} searchTerm - Term to search for
 * @returns {Array} Filtered articles containing the search term with visible matches
 */
export function searchArticles(articles, searchTerm) {
  if (!searchTerm || !searchTerm.trim()) {
    return articles;
  }

  return articles.filter(article => {
    const matches = getArticleMatches(article, searchTerm);

    // Only include articles that have at least one visible match:
    // - Title contains the term, OR
    // - Has matching sentences in content/description, OR
    // - Has matching categories
    return matches.titleMatches ||
           matches.contentMatches.length > 0 ||
           matches.descriptionMatches.length > 0 ||
           matches.categoryMatches.length > 0;
  });
}

/**
 * Strips HTML tags from content
 * @param {string} html - HTML string
 * @returns {string} Plain text
 */
export function stripHtmlTags(html) {
  const div = document.createElement('div');
  div.innerHTML = html;
  return div.textContent || div.innerText || '';
}

/**
 * Finds all sentences containing the search term
 * @param {string} text - Text to search in
 * @param {string} searchTerm - Term to search for
 * @returns {Array} Array of sentences containing the search term
 */
export function findMatchingSentences(text, searchTerm) {
  if (!searchTerm || !searchTerm.trim() || !text) {
    return [];
  }

  const term = searchTerm.toLowerCase().trim();

  // Split text into sentences - handle multiple sentence ending patterns
  // Match sentences ending with . ! ? or newlines, or just chunks of text
  let sentences = text.match(/[^.!?\n]+[.!?\n]+/g);

  // If no proper sentences found, split by newlines or use whole text
  if (!sentences || sentences.length === 0) {
    sentences = text.split(/\n+/).filter(s => s.trim().length > 0);
    if (sentences.length === 0) {
      sentences = [text];
    }
  }

  const matches = [];

  sentences.forEach(sentence => {
    const cleanSentence = sentence.trim();
    if (cleanSentence && cleanSentence.toLowerCase().includes(term)) {
      // Limit sentence length for display
      if (cleanSentence.length > 500) {
        // Find the term and show context around it
        const lowerSentence = cleanSentence.toLowerCase();
        const termIndex = lowerSentence.indexOf(term);
        const start = Math.max(0, termIndex - 150);
        const end = Math.min(cleanSentence.length, termIndex + term.length + 150);
        const snippet = (start > 0 ? '...' : '') +
                       cleanSentence.substring(start, end) +
                       (end < cleanSentence.length ? '...' : '');
        matches.push(snippet);
      } else {
        matches.push(cleanSentence);
      }
    }
  });

  return matches;
}

/**
 * Gets all matching contexts from an article
 * @param {Object} article - Article object
 * @param {string} searchTerm - Term to search for
 * @returns {Object} Object containing matches from title, content, and description
 */
export function getArticleMatches(article, searchTerm) {
  if (!searchTerm || !searchTerm.trim()) {
    return { titleMatches: false, contentMatches: [], descriptionMatches: [], categoryMatches: [] };
  }

  const term = searchTerm.toLowerCase().trim();

  // Check if title matches
  const titleMatches = article.title.toLowerCase().includes(term);

  // Get matching sentences from content
  const plainContent = stripHtmlTags(article.content || '');
  const contentMatches = findMatchingSentences(plainContent, searchTerm);

  // Get matching sentences from description
  const plainDescription = stripHtmlTags(article.description || '');
  const descriptionMatches = findMatchingSentences(plainDescription, searchTerm);

  // Get matching categories
  const categoryMatches = article.categories.filter(cat =>
    cat.toLowerCase().includes(term)
  );

  return {
    titleMatches,
    contentMatches,
    descriptionMatches,
    categoryMatches
  };
}
