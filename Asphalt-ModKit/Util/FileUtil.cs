/** 
 * ------------------------------------
 * Copyright (c) 2018 [Kronox]
 * See LICENSE file in the project root for full license information.
 * ------------------------------------
 * Created by Kronox on March 26, 2018
 * ------------------------------------
 **/

using Eco.Core.Plugins.Interfaces;
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
            //create File if not existant
            if (!File.Exists(filePath + fileName))
            {
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

        public static string GetModFolder(AsphaltMod mod)
        {
            return MODS_DIR + "/" + mod.ToString() + "/";
        }
    }
}
