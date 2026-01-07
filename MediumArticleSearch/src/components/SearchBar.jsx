import './SearchBar.css';

function SearchBar({ onUsernameChange, username, onFileImport }) {
  const handleUsernameSubmit = (e) => {
    e.preventDefault();
    onUsernameChange();
  };

  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      onFileImport(file);
    }
  };

  return (
    <div className="search-container">
      <div className="import-section">
        <div className="import-info">
          <strong>Option 1:</strong> Import ALL your articles (FREE forever)
        </div>
        <div className="file-upload">
          <label htmlFor="file-input" className="file-label">
            <span>📁 Import Medium Export (ZIP)</span>
            <input
              id="file-input"
              type="file"
              accept=".zip"
              onChange={handleFileChange}
              className="file-input"
            />
          </label>
          <a
            href="https://medium.com/me/settings/security"
            target="_blank"
            rel="noopener noreferrer"
            className="help-link"
          >
            How to export?
          </a>
        </div>
      </div>

      <div className="divider">
        <span>OR</span>
      </div>

      <div className="rss-section">
        <div className="rss-info">
          <strong>Option 2:</strong> Fetch from RSS (limited to 10 recent articles)
        </div>
        <form onSubmit={handleUsernameSubmit} className="username-form">
          <input
            type="text"
            placeholder="Enter your Medium username (e.g., @username)"
            value={username}
            onChange={(e) => onUsernameChange(e.target.value, false)}
            className="username-input"
          />
          <button type="submit" className="fetch-btn">
            Fetch from RSS
          </button>
        </form>
      </div>

    </div>
  );
}

export default SearchBar;
