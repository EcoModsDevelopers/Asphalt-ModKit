﻿using Eco.Core.Serialization;

namespace Asphalt.api.util
{
    public static class ClassSerializer<T> where T : class, new()
    {
        public static T Deserialize(string filePath, string fileName)
        {
            string content = FileUtil.ReadFromFile(filePath, fileName);

            if (string.IsNullOrWhiteSpace(content))
                return new T();

            return SerializationUtils.DeserializeJson<T>(content);
        }

        public static void Serialize(string filePath, string fileName, T clazz)
        {
            string content = SerializationUtils.SerializeRawJsonIndented(clazz);
            FileUtil.WriteToFile(filePath, fileName, content);
        }
    }
}
