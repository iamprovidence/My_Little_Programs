@echo off
echo Starting Medium Article Search...
echo.

REM Check if node_modules exists
if not exist "node_modules\" (
    echo node_modules not found. Installing dependencies...
    echo.
    call npm install
    echo.
)

echo Starting development server...
echo.
call npm run dev

pause
