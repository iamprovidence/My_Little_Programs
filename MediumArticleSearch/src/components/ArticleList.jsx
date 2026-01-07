import ArticleCard from './ArticleCard';
import './ArticleList.css';

function ArticleList({ articles, searchTerm, totalArticles }) {
  if (!articles || articles.length === 0) {
    return (
      <div className="no-results">
        {searchTerm ? (
          <>
            <p>No articles found matching "{searchTerm}"</p>
            {totalArticles > 0 && (
              <p className="total-info">Searched through {totalArticles} article{totalArticles !== 1 ? 's' : ''}</p>
            )}
          </>
        ) : (
          <p>Enter your Medium username and click "Fetch Articles" to get started</p>
        )}
      </div>
    );
  }

  return (
    <div className="article-list">
      <div className="results-info">
        <p>
          Found <strong>{articles.length}</strong> article{articles.length !== 1 ? 's' : ''}
          {searchTerm && (
            <>
              {' '}matching "<strong>{searchTerm}</strong>"
              {totalArticles > 0 && articles.length < totalArticles && (
                <> (out of {totalArticles} total)</>
              )}
            </>
          )}
        </p>
      </div>
      <div className="articles-grid">
        {articles.map((article, index) => (
          <ArticleCard key={index} article={article} searchTerm={searchTerm} />
        ))}
      </div>
    </div>
  );
}

export default ArticleList;
