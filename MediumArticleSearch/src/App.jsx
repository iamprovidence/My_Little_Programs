import { useState } from 'react'
import './App.css'
import SearchBar from './components/SearchBar'
import ArticleList from './components/ArticleList'
import { fetchMediumArticles, searchArticles } from './services/mediumService'
import { parseMediumExport } from './services/importService'

function App() {
  const [step, setStep] = useState(1) // 1 = load articles, 2 = search
  const [username, setUsername] = useState('')
  const [allArticles, setAllArticles] = useState([])
  const [filteredArticles, setFilteredArticles] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const [searchInput, setSearchInput] = useState('') // What user is typing
  const [currentSearchTerm, setCurrentSearchTerm] = useState('') // Actual search term applied

  const handleUsernameChange = async (value, shouldFetch = true) => {
    if (value !== undefined) {
      setUsername(value)
    }

    if (!shouldFetch) return

    const usernameToFetch = value !== undefined ? value : username

    if (!usernameToFetch.trim()) {
      setError('Please enter a Medium username')
      return
    }

    setLoading(true)
    setError(null)

    try {
      const articles = await fetchMediumArticles(usernameToFetch)
      setAllArticles(articles)
      setFilteredArticles(articles)
      setCurrentSearchTerm('')
      setStep(2) // Move to search step
    } catch (err) {
      setError(err.message || 'Failed to fetch articles. Please check the username and try again.')
      setAllArticles([])
      setFilteredArticles([])
    } finally {
      setLoading(false)
    }
  }

  const handleSearch = () => {
    // Only search when button is clicked or Enter is pressed
    setCurrentSearchTerm(searchInput)
    const results = searchArticles(allArticles, searchInput)
    setFilteredArticles(results)
  }

  const handleFileImport = async (file) => {
    setLoading(true)
    setError(null)

    try {
      const articles = await parseMediumExport(file)
      setAllArticles(articles)
      setFilteredArticles(articles)
      setCurrentSearchTerm('')
      setStep(2) // Move to search step
    } catch (err) {
      setError(err.message || 'Failed to import file. Please ensure it is a valid Medium export ZIP file.')
      setAllArticles([])
      setFilteredArticles([])
    } finally {
      setLoading(false)
    }
  }

  const handleBackToStep1 = () => {
    setStep(1)
    setSearchInput('')
    setCurrentSearchTerm('')
    setFilteredArticles(allArticles)
  }

  return (
    <div className="app">
      <header className="app-header">
        <h1>Medium Article Search</h1>
        {step === 1 && <p>Load your Medium articles to get started</p>}
        {step === 2 && <p>Search through your {allArticles.length} articles</p>}
      </header>

      <main className="app-main">
        {step === 1 && (
          <>
            <div className="step-indicator">
              <span className="step-badge active">Step 1: Load Articles</span>
              <span className="step-badge">Step 2: Search</span>
            </div>

            <SearchBar
              username={username}
              onUsernameChange={handleUsernameChange}
              onFileImport={handleFileImport}
            />

            {loading && (
              <div className="loading">
                <div className="spinner"></div>
                <p>Loading articles...</p>
              </div>
            )}

            {error && (
              <div className="error">
                <p>{error}</p>
              </div>
            )}
          </>
        )}

        {step === 2 && (
          <>
            <div className="step-indicator">
              <span className="step-badge completed">Step 1: Load Articles ✓</span>
              <span className="step-badge active">Step 2: Search</span>
            </div>

            <button onClick={handleBackToStep1} className="back-button">
              ← Load Different Articles
            </button>

            <div className="search-step">
              <form onSubmit={(e) => { e.preventDefault(); handleSearch(); }} className="search-only-form">
                <input
                  type="text"
                  placeholder='Enter keyword to search (e.g., "business logic", "DDD")'
                  value={searchInput}
                  onChange={(e) => setSearchInput(e.target.value)}
                  className="search-only-input"
                  autoFocus
                />
                <button type="submit" className="search-only-btn">
                  Search
                </button>
              </form>
            </div>

            <ArticleList
              articles={filteredArticles}
              searchTerm={currentSearchTerm}
              totalArticles={allArticles.length}
            />
          </>
        )}
      </main>
    </div>
  )
}

export default App
