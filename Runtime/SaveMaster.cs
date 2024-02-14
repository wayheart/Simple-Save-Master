using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Simple.Save.Master.Runtime
{
    public static class SaveMaster
    {
        /// <summary>
        /// A method to save data to a file
        /// </summary>
        /// <typeparam name="T">Data type to save</typeparam>
        /// <param name="saveData">Data to save</param>
        public static void Save<T>(T saveData)
        {
            string filePath = GetFilePath(typeof(T).Name);

            string jsonDataString = JsonConvert.SerializeObject(saveData, Formatting.Indented);

            File.WriteAllText(filePath, jsonDataString);
        }

        /// <summary>
        /// A method to load data from a file, if not, then returns a new instance
        /// </summary>
        /// <typeparam name="T">Data type to load</typeparam>
        /// <returns>Loaded data of type T</returns>
        public static T Load<T>() where T : new()
        {
            string filePath = GetFilePath(typeof(T).Name);

            if (!Exists<T>())
            {
                return new T();
            }

            string jsonDataString = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<T>(jsonDataString);
        }

        /// <summary>
        /// A method checks if this file exists or not
        /// </summary>
        /// <param T="T">Key to identify the file(typeof)</param>
        /// <returns>File exists or not</returns>
        public static bool Exists<T>()
        {
            return File.Exists(GetFilePath(typeof(T).Name));
        }

        /// <summary>
        /// A method for getting the file path
        /// </summary>
        /// <param name="key">Key to identify the file(nameof)</param>
        /// <returns>File path</returns>
        private static string GetFilePath(string key)
        {
            Debug.Log(Path.Combine(Application.persistentDataPath, key + ".json"));
            return Path.Combine(Application.persistentDataPath, key + ".json");
        }
    }
}
