/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 26, 2018
 * ------------------------------------
 **/

using System.IO;

namespace Asphalt.Api.Util
{
    public static class FileUtil
    {
        public static readonly string MODS_DIR = "Mods";

        public static void CreateDirectoryAndFile(string filePath, string fileName)
        {
            CreateDirectory(filePath);
            CreateFile(filePath, fileName);
        }

        public static void CreateDirectory(string filePath)
        {
            //create Directory if not existant
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
        }

        public static void CreateFile(string filePath, string fileName)
        {
            string file = Path.Combine(filePath, fileName);

            if (!File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
                fs.Close();
            }
        }

        public static string ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                return null;

            return File.ReadAllText(fileName);
        }

        public static void WriteToFile(string fileName, string content)
        {
            CreateDirectoryAndFile(Path.GetDirectoryName(fileName), Path.GetFileName(fileName));

            File.WriteAllText(fileName, content);
        }
    }
}
