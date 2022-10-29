int counter = GetLastValue();

while (true)
{
    Console.WriteLine($"Counter {counter++}");
    SaveValue(counter);
    Thread.Sleep(1000);
}

int GetLastValue()
{
    var filePath = GetFilePath();
    var value = File.ReadAllText(filePath);

    return int.Parse(value);
}

void SaveValue(int value)
{
    var filePath = GetFilePath();
    File.WriteAllText(filePath, value.ToString());
}

static string GetFilePath()
{
    return Environment.GetEnvironmentVariable("FilePath") ?? @"C:\<full-path>\data\file.txt";
}