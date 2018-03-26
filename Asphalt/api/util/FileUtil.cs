using System.IO;

namespace Asphalt.api.util
{
    public static class FileUtil
    {
        public static void CreateDirectoryAndFile(string filePath, string fileName)
        {
            //create Directory if not existant
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            //create File if not existant
            if (File.Exists(filePath + fileName)) {
                FileStream fs = new FileStream(filePath + fileName, FileMode.OpenOrCreate);
                fs.Close();
            }
        }

        public static string ReadFromFile(string filePath, string fileName)
        {
            CreateDirectoryAndFile(filePath, fileName);

            return File.ReadAllText(filePath + fileName);
        }

        public static void WriteToFile(string filePath, string fileName, string content)
        {
            CreateDirectoryAndFile(filePath, fileName);

            File.WriteAllText(filePath + fileName, content);
        }
    }
}
