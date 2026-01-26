import { useState, useEffect } from 'react';

const ApiKeyInput = ({ onApiKeyChange, usageStats }) => {
  const [apiKey, setApiKey] = useState('');
  const [showKey, setShowKey] = useState(false);
  const [isExpanded, setIsExpanded] = useState(false);

  useEffect(() => {
    // Load API key from localStorage on mount
    const savedKey = localStorage.getItem('gemini_api_key');
    if (savedKey) {
      setApiKey(savedKey);
      onApiKeyChange(savedKey);
    } else {
      // If no saved key, expand the input
      setIsExpanded(true);
    }
  }, []);

  const handleApiKeyChange = (e) => {
    const newKey = e.target.value;
    setApiKey(newKey);

    // Save to localStorage
    if (newKey.trim()) {
      localStorage.setItem('gemini_api_key', newKey.trim());
      onApiKeyChange(newKey.trim());
    } else {
      localStorage.removeItem('gemini_api_key');
      onApiKeyChange('');
    }
  };

  const handleClear = () => {
    setApiKey('');
    localStorage.removeItem('gemini_api_key');
    onApiKeyChange('');
    setIsExpanded(true);
  };

  return (
    <div className="bg-blue-50 border border-blue-200 rounded-lg p-4 mb-6">
      <div className="flex items-start justify-between mb-2">
        <div className="flex items-center gap-2">
          <svg
            className="w-5 h-5 text-blue-600"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M15 7a2 2 0 012 2m4 0a6 6 0 01-7.743 5.743L11 17H9v2H7v2H4a1 1 0 01-1-1v-2.586a1 1 0 01.293-.707l5.964-5.964A6 6 0 1121 9z"
            />
          </svg>
          <h3 className="text-sm font-semibold text-gray-800">Google Gemini API Key</h3>
        </div>

        {apiKey && !isExpanded && (
          <button
            onClick={() => setIsExpanded(!isExpanded)}
            className="text-sm text-blue-600 hover:text-blue-800"
          >
            {isExpanded ? 'Hide' : 'Edit'}
          </button>
        )}
      </div>

      {(!apiKey || isExpanded) && (
        <>
          <div className="flex gap-2 mb-2">
            <div className="flex-1 relative">
              <input
                type={showKey ? 'text' : 'password'}
                value={apiKey}
                onChange={handleApiKeyChange}
                placeholder="AIza..."
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-primary focus:border-transparent text-sm font-mono"
              />
              {apiKey && (
                <button
                  onClick={() => setShowKey(!showKey)}
                  className="absolute right-2 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-700"
                  title={showKey ? 'Hide key' : 'Show key'}
                >
                  {showKey ? (
                    <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth={2}
                        d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"
                      />
                    </svg>
                  ) : (
                    <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth={2}
                        d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"
                      />
                      <path
                        strokeLinecap="round"
                        strokeLinejoin="round"
                        strokeWidth={2}
                        d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"
                      />
                    </svg>
                  )}
                </button>
              )}
            </div>

            {apiKey && (
              <button
                onClick={handleClear}
                className="px-3 py-2 bg-red-100 text-red-700 rounded-md hover:bg-red-200 transition-colors text-sm"
                title="Clear API key"
              >
                Clear
              </button>
            )}

            {apiKey && (
              <button
                onClick={() => setIsExpanded(false)}
                className="px-3 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors text-sm"
              >
                Done
              </button>
            )}
          </div>

          <p className="text-xs text-gray-600 leading-relaxed">
            Enter your Google Gemini API key to start rephrasing text. Your key is stored locally in your browser.{' '}
            <a
              href="https://aistudio.google.com/app/apikey"
              target="_blank"
              rel="noopener noreferrer"
              className="text-blue-600 hover:text-blue-800 underline font-medium"
            >
              Get your free API key here
            </a>
          </p>
        </>
      )}

      {apiKey && !isExpanded && (
        <div className="space-y-2">
          <div className="flex items-center gap-2">
            <span className="text-sm text-green-700 font-medium">✓ API Key configured</span>
            <span className="text-xs text-gray-500 font-mono">
              {apiKey.substring(0, 10)}...{apiKey.substring(apiKey.length - 4)}
            </span>
          </div>

          {/* Usage Stats */}
          {usageStats && (
            <div className="flex items-center gap-4 text-xs bg-white rounded px-3 py-2 border border-gray-200">
              <div className="flex items-center gap-1.5">
                <svg
                  className="w-4 h-4 text-blue-600"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z"
                  />
                </svg>
                <span className="font-medium text-gray-700">API Usage:</span>
              </div>

              <div className="flex items-center gap-1">
                <span className="text-gray-600">Today:</span>
                <span className="font-semibold text-blue-600">
                  {usageStats.requestsToday}
                </span>
                <span className="text-gray-500">/ 1500</span>
              </div>

              <div className="flex items-center gap-1">
                <span className="text-gray-600">Total:</span>
                <span className="font-semibold text-gray-700">
                  {usageStats.totalRequests}
                </span>
              </div>

              <div className="ml-auto text-green-600 font-medium">
                100% FREE
              </div>
            </div>
          )}
        </div>
      )}
    </div>
  );
};

export default ApiKeyInput;
