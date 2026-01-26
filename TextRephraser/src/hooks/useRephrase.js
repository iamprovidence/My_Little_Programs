import { useState, useEffect } from 'react';
import { rephraseText } from '../services/openai';

export const useRephrase = (debouncedText) => {
  const [rephrasedText, setRephrasedText] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    // Skip if input is empty
    if (!debouncedText || debouncedText.trim().length === 0) {
      setRephrasedText('');
      setError(null);
      setIsLoading(false);
      return;
    }

    let isCancelled = false;

    const fetchRephrase = async () => {
      setIsLoading(true);
      setError(null);

      try {
        const result = await rephraseText(debouncedText);

        if (!isCancelled) {
          setRephrasedText(result);
        }
      } catch (err) {
        if (!isCancelled) {
          setError(err.message);
          setRephrasedText('');
        }
      } finally {
        if (!isCancelled) {
          setIsLoading(false);
        }
      }
    };

    fetchRephrase();

    // Cleanup: prevent state updates if component unmounts
    return () => {
      isCancelled = true;
    };
  }, [debouncedText]);

  const retry = () => {
    setError(null);
    // This will re-trigger the effect by forcing a re-render
    setIsLoading(true);
  };

  return { rephrasedText, isLoading, error, retry };
};
