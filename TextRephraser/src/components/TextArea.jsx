import { MAX_CHAR_LENGTH } from '../utils/constants';

const TextArea = ({
  value,
  onChange,
  placeholder,
  readOnly = false,
  label,
  onClear,
  onCopy,
  showActions = false
}) => {
  const charCount = value.length;
  const isNearLimit = charCount > MAX_CHAR_LENGTH * 0.9;
  const isOverLimit = charCount > MAX_CHAR_LENGTH;

  const handleCopy = async () => {
    try {
      await navigator.clipboard.writeText(value);
      // You could add a toast notification here
    } catch (err) {
      console.error('Failed to copy text:', err);
    }
    if (onCopy) onCopy();
  };

  return (
    <div className="flex flex-col h-full">
      <div className="flex items-center justify-between mb-2">
        <label className="text-sm font-medium text-gray-700">{label}</label>
        {!readOnly && (
          <span
            className={`text-xs ${
              isOverLimit
                ? 'text-red-600 font-semibold'
                : isNearLimit
                ? 'text-orange-600'
                : 'text-gray-500'
            }`}
          >
            {charCount} / {MAX_CHAR_LENGTH}
          </span>
        )}
      </div>

      <div className="relative flex-1">
        <textarea
          value={value}
          onChange={onChange}
          readOnly={readOnly}
          placeholder={placeholder}
          maxLength={readOnly ? undefined : MAX_CHAR_LENGTH}
          className={`w-full h-full min-h-[300px] p-4 rounded-lg border resize-none focus:outline-none focus:ring-2 focus:ring-primary focus:border-transparent transition-all ${
            readOnly
              ? 'bg-gray-50 text-gray-800 cursor-default'
              : 'bg-white text-gray-900'
          } ${isOverLimit ? 'border-red-500' : 'border-gray-300'}`}
          style={{ fontSize: '16px', lineHeight: '1.5' }}
        />

        {showActions && value && (
          <div className="absolute bottom-4 right-4 flex gap-2">
            {!readOnly && onClear && (
              <button
                onClick={onClear}
                className="px-3 py-1.5 bg-white border border-gray-300 rounded-md text-sm text-gray-700 hover:bg-gray-50 hover:border-gray-400 transition-colors shadow-sm"
                title="Clear text"
              >
                Clear
              </button>
            )}
            {readOnly && (
              <button
                onClick={handleCopy}
                className="px-3 py-1.5 bg-primary text-white rounded-md text-sm hover:bg-blue-700 transition-colors shadow-sm flex items-center gap-1.5"
                title="Copy to clipboard"
              >
                <svg
                  className="w-4 h-4"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M8 16H6a2 2 0 01-2-2V6a2 2 0 012-2h8a2 2 0 012 2v2m-6 12h8a2 2 0 002-2v-8a2 2 0 00-2-2h-8a2 2 0 00-2 2v8a2 2 0 002 2z"
                  />
                </svg>
                Copy
              </button>
            )}
          </div>
        )}
      </div>
    </div>
  );
};

export default TextArea;
