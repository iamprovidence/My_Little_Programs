import { useState, useEffect } from 'react';
import { rephraseText, incrementUsage, getUsageStats } from '../services/gemini';
import ApiKeyInput from './ApiKeyInput';
import TextArea from './TextArea';
import LoadingSpinner from './LoadingSpinner';
import ErrorMessage from './ErrorMessage';

const RephraseContainer = () => {
  const [apiKey, setApiKey] = useState('');
  const [inputText, setInputText] = useState('');
  const [rephrasedText, setRephrasedText] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [usageStats, setUsageStats] = useState(getUsageStats());
  const [history, setHistory] = useState([]);

  // Load history from localStorage on mount
  useEffect(() => {
    const savedHistory = localStorage.getItem('rephrase_history');
    if (savedHistory) {
      setHistory(JSON.parse(savedHistory));
    }
  }, []);

  const handleInputChange = (e) => {
    setInputText(e.target.value);
  };

  const handleClear = () => {
    setInputText('');
    setRephrasedText('');
    setError(null);
  };

  const handleCopy = () => {
    console.log('Text copied to clipboard!');
  };

  const handleRephrase = async () => {
    if (!inputText.trim()) {
      return;
    }

    if (!apiKey) {
      setError('Please enter your Google Gemini API key above.');
      return;
    }

    setIsLoading(true);
    setError(null);
    setRephrasedText('');

    try {
      const result = await rephraseText(inputText, apiKey);
      setRephrasedText(result);

      // Track usage after successful rephrase
      const newStats = incrementUsage();
      setUsageStats(newStats);

      // Add to history
      const newHistoryItem = {
        id: Date.now(),
        original: inputText,
        rephrased: result,
        timestamp: new Date().toISOString()
      };

      const updatedHistory = [newHistoryItem, ...history].slice(0, 10); // Keep last 10 items
      setHistory(updatedHistory);
      localStorage.setItem('rephrase_history', JSON.stringify(updatedHistory));

    } catch (err) {
      setError(err.message);
    } finally {
      setIsLoading(false);
    }
  };

  const handleApiKeyChange = (newKey) => {
    setApiKey(newKey);
  };

  const handleClearHistory = () => {
    setHistory([]);
    localStorage.removeItem('rephrase_history');
  };

  const handleLoadFromHistory = (item) => {
    setInputText(item.original);
    setRephrasedText(item.rephrased);
  };

  return (
    <div className="max-w-7xl mx-auto px-6 py-8">
      {/* API Key Input */}
      <ApiKeyInput onApiKeyChange={handleApiKeyChange} usageStats={usageStats} />

      {/* Rephrase Button - Moved to Top */}
      <div className="mb-6 text-center">
        <button
          onClick={handleRephrase}
          disabled={!inputText.trim() || isLoading || !apiKey}
          className={`px-8 py-3 rounded-lg font-medium text-white transition-all shadow-md ${
            !inputText.trim() || isLoading || !apiKey
              ? 'bg-gray-400 cursor-not-allowed'
              : 'bg-primary hover:bg-blue-700 hover:shadow-lg transform hover:scale-105'
          }`}
        >
          {isLoading ? (
            <span className="flex items-center gap-2">
              <svg
                className="animate-spin h-5 w-5"
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
              >
                <circle
                  className="opacity-25"
                  cx="12"
                  cy="12"
                  r="10"
                  stroke="currentColor"
                  strokeWidth="4"
                ></circle>
                <path
                  className="opacity-75"
                  fill="currentColor"
                  d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
                ></path>
              </svg>
              Rephrasing...
            </span>
          ) : (
            <span className="flex items-center gap-2">
              <svg
                className="w-5 h-5"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M4 4v5h.582m15.356 2A8.001 8.001 0 004.582 9m0 0H9m11 11v-5h-.581m0 0a8.003 8.003 0 01-15.357-2m15.357 2H15"
                />
              </svg>
              Rephrase Text
            </span>
          )}
        </button>
        {!apiKey && (
          <p className="text-sm text-red-600 mt-2">
            ⚠️ Please enter your API key above to enable rephrasing
          </p>
        )}
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        {/* Input Area */}
        <div className="bg-white rounded-lg shadow-md p-6">
          <TextArea
            value={inputText}
            onChange={handleInputChange}
            onClear={handleClear}
            placeholder="Enter text to rephrase..."
            label="Original Text"
            showActions={inputText.length > 0}
          />
        </div>

        {/* Output Area */}
        <div className="bg-white rounded-lg shadow-md p-6 relative">
          <TextArea
            value={rephrasedText}
            readOnly={true}
            onCopy={handleCopy}
            placeholder={
              isLoading
                ? ''
                : inputText.length > 0
                ? 'Click "Rephrase" to see the rephrased version'
                : 'Enter text on the left and click "Rephrase"'
            }
            label="Rephrased Text"
            showActions={rephrasedText.length > 0}
          />

          {isLoading && <LoadingSpinner />}
          {error && <ErrorMessage message={error} onRetry={handleRephrase} />}
        </div>
      </div>

      {/* History Section */}
      {history.length > 0 && (
        <div className="mt-8">
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-xl font-bold text-gray-800 flex items-center gap-2">
              <svg
                className="w-6 h-6 text-primary"
                fill="none"
                stroke="currentColor"
                viewBox="0 0 24 24"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z"
                />
              </svg>
              History
            </h2>
            <button
              onClick={handleClearHistory}
              className="text-sm text-red-600 hover:text-red-800 font-medium"
            >
              Clear History
            </button>
          </div>

          <div className="space-y-4">
            {history.map((item) => (
              <div
                key={item.id}
                className="bg-white rounded-lg shadow-md p-4 hover:shadow-lg transition-shadow cursor-pointer"
                onClick={() => handleLoadFromHistory(item)}
              >
                <div className="flex items-start justify-between mb-2">
                  <span className="text-xs text-gray-500">
                    {new Date(item.timestamp).toLocaleString()}
                  </span>
                  <button
                    onClick={(e) => {
                      e.stopPropagation();
                      handleLoadFromHistory(item);
                    }}
                    className="text-xs text-blue-600 hover:text-blue-800 font-medium"
                  >
                    Load
                  </button>
                </div>

                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div>
                    <p className="text-xs font-semibold text-gray-600 mb-1">Original:</p>
                    <p className="text-sm text-gray-800 line-clamp-3">
                      {item.original}
                    </p>
                  </div>

                  <div>
                    <p className="text-xs font-semibold text-gray-600 mb-1">Rephrased:</p>
                    <p className="text-sm text-gray-800 line-clamp-3">
                      {item.rephrased}
                    </p>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default RephraseContainer;
