cd ..
docker build --tag counter-with-file-image .

:: persists data until container is not disposed
docker create --env FilePath=/app/data/file.txt --name counter-with-file-container counter-with-file-image 
docker start counter-with-file-container --attach --interactive

:: uncomment to start counting from beginning
::docker rm counter-with-file-container
