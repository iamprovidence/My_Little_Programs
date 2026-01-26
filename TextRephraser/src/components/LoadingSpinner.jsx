const LoadingSpinner = () => {
  return (
    <div className="absolute inset-0 flex items-center justify-center bg-white bg-opacity-75 rounded-lg">
      <div className="flex flex-col items-center gap-2">
        <div className="w-8 h-8 border-4 border-primary border-t-transparent rounded-full animate-spin"></div>
        <p className="text-sm text-secondary">Rephrasing...</p>
      </div>
    </div>
  );
};

export default LoadingSpinner;
