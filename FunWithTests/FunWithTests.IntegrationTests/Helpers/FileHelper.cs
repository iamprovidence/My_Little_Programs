using System.IO;
using System.Linq;

namespace FunWithTests.IntegrationTests.Helpers
{
    internal static class FileHelper
    {
        public static string ReverseGetFile(string fileName)
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles(fileName).Any())
            {
                directory = directory.Parent;
            }

            if (directory == null)
            {
                throw new FileNotFoundException("Could not find docker compose file");
            }

            return Path.Combine(directory.FullName, fileName);
        }
    }
}
