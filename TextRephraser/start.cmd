@echo off
echo Starting Text Rephraser App...
echo.

if not exist "node_modules" (
    echo Installing dependencies...
    call npm install
    if %errorlevel% neq 0 (
        echo.
        echo ERROR: npm install failed!
        pause
        exit /b %errorlevel%
    )
    echo.
) else (
    echo Dependencies already installed.
    echo.
)

echo Starting development server...
call npm run dev

echo.
echo Server stopped.
pause
