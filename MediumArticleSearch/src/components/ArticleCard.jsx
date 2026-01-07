import { getArticleMatches } from '../services/mediumService';
import './ArticleCard.css';

function ArticleCard({ article, searchTerm }) {
  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  };

  const highlightText = (text, term) => {
    if (!term) return text;

    const escapedTerm = term.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
    const parts = text.split(new RegExp(`(${escapedTerm})`, 'gi'));
    return parts.map((part, index) =>
      part.toLowerCase() === term.toLowerCase() ? (
        <mark key={index}>{part}</mark>
      ) : (
        part
      )
    );
  };

  const matches = searchTerm ? getArticleMatches(article, searchTerm) : null;

  // Combine and deduplicate matches from content and description
  const allMatches = matches ? [
    ...matches.contentMatches,
    ...matches.descriptionMatches
  ] : [];

  // Remove duplicates by comparing normalized sentences
  const uniqueMatches = allMatches.filter((sentence, index, self) => {
    const normalized = sentence.trim().toLowerCase();
    return self.findIndex(s => s.trim().toLowerCase() === normalized) === index;
  });

  return (
    <div className="article-item">
      <div className="article-header">
        <h3 className="article-title">
          <a href={article.link} target="_blank" rel="noopener noreferrer">
            {searchTerm && matches?.titleMatches
              ? highlightText(article.title, searchTerm)
              : article.title}
          </a>
        </h3>
        <div className="article-meta">
          <span className="article-author">{article.author}</span>
          <span className="article-separator">•</span>
          <span className="article-date">{formatDate(article.pubDate)}</span>
        </div>
      </div>

      {searchTerm && matches && (uniqueMatches.length > 0 || matches.categoryMatches.length > 0) && (
        <div className="article-matches">
          {uniqueMatches.length > 0 && (
            <div className="match-section">
              <div className="match-label">Matches ({uniqueMatches.length}):</div>
              {uniqueMatches.map((sentence, index) => (
                <div key={`match-${index}`} className="match-item">
                  <span className="match-bullet">•</span>
                  <span className="match-text">{highlightText(sentence, searchTerm)}</span>
                </div>
              ))}
            </div>
          )}

          {matches.categoryMatches.length > 0 && (
            <div className="match-section">
              <div className="match-label">Matching tags:</div>
              <div className="article-tags">
                {matches.categoryMatches.map((tag, index) => (
                  <span key={index} className="tag tag-highlight">
                    {highlightText(tag, searchTerm)}
                  </span>
                ))}
              </div>
            </div>
          )}
        </div>
      )}

      {article.link && (
        <a
          href={article.link}
          target="_blank"
          rel="noopener noreferrer"
          className="read-more"
        >
          Read full article on Medium →
        </a>
      )}
      {!article.link && (
        <div className="no-link">
          Link not available (imported from export)
        </div>
      )}
    </div>
  );
}

export default ArticleCard;
