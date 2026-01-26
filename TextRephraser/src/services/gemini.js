import { GoogleGenAI } from '@google/genai';

export const rephraseText = async (text, apiKey) => {
  if (!text || text.trim().length === 0) {
    return '';
  }

  if (!apiKey) {
    throw new Error('Google Gemini API key is not configured. Please enter your API key above.');
  }

  // Try multiple model names in order (newest to oldest)
  const modelNamesToTry = [
    'gemini-2.5-flash'
  ];

  let lastError = null;

  for (const modelName of modelNamesToTry) {
    try {
      console.log(`Trying model: ${modelName}`);

      const ai = new GoogleGenAI({ apiKey: apiKey });

      const prompt = `Rephrase the following text, while preserving the original meaning. Respond only with the rephrased text, no explanations or additional commentary.

Text to rephrase: ${text}`;

      const response = await ai.models.generateContent({
        model: modelName,
        contents: [{ role: 'user', parts: [{ text: prompt }] }],
      });

      const rephrased = response.text;

      console.log(`✅ Success with model: ${modelName}`);
      return rephrased;

    } catch (error) {
      console.warn(`❌ Failed with model ${modelName}:`, error.message);
      lastError = error;
      // Continue to next model
    }
  }

  // If all models failed, throw the last error
  console.error('Google Gemini API Error - All models failed:', lastError);
  console.error('Error details:', {
    message: lastError?.message,
    status: lastError?.status,
    statusText: lastError?.statusText
  });

  // Provide user-friendly error messages
  const errorMessage = lastError?.message || '';

  if (errorMessage.includes('API_KEY_INVALID') || errorMessage.includes('API key not valid') || lastError?.status === 400) {
    throw new Error('Invalid API key. Please get a new key from https://aistudio.google.com/app/apikey');
  } else if (lastError?.status === 403 || errorMessage.includes('403')) {
    throw new Error('API access forbidden. Please create a NEW API key at https://aistudio.google.com/app/apikey');
  } else if (lastError?.status === 404 || errorMessage.includes('404') || errorMessage.includes('not found')) {
    throw new Error('Model not found. Your API key might not have access to Gemini models. Please create a new API key at https://aistudio.google.com/app/apikey');
  } else if (lastError?.status === 429 || errorMessage.includes('RESOURCE_EXHAUSTED') || errorMessage.includes('429')) {
    throw new Error('Rate limit exceeded (15 requests/minute). Please wait a moment and try again.');
  } else if (lastError?.status === 500 || errorMessage.includes('500')) {
    throw new Error('Google API server error. Please try again in a moment.');
  } else if (errorMessage.includes('fetch') || errorMessage.includes('Failed to fetch')) {
    throw new Error('Cannot connect to Google API. Check your internet connection or try again later.');
  } else {
    throw new Error(`API Error: ${errorMessage || 'Failed to rephrase text. Please create a new API key at https://aistudio.google.com/app/apikey'}`);
  }
};

// Usage tracking functions
export const getUsageStats = () => {
  const stats = localStorage.getItem('gemini_usage_stats');
  if (!stats) {
    return {
      requestsToday: 0,
      lastResetDate: new Date().toDateString(),
      totalRequests: 0
    };
  }
  return JSON.parse(stats);
};

export const incrementUsage = () => {
  const stats = getUsageStats();
  const today = new Date().toDateString();

  // Reset daily count if it's a new day
  if (stats.lastResetDate !== today) {
    stats.requestsToday = 0;
    stats.lastResetDate = today;
  }

  stats.requestsToday += 1;
  stats.totalRequests += 1;

  localStorage.setItem('gemini_usage_stats', JSON.stringify(stats));
  return stats;
};

export const resetUsageStats = () => {
  localStorage.removeItem('gemini_usage_stats');
  return getUsageStats();
};
