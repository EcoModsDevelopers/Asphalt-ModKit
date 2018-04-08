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
        public static void CreateDirectoryAndFile(string filePath, string fileName)
        {
            //create Directory if not existant
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            //create File if not existant
            if (!File.Exists(filePath + fileName)) {
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
