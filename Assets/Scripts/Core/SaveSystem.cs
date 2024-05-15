using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Muvuca.Core
{
    
    public static class SaveSystem
    {
        public static int CurrentSaveSlot;

        public static string SaveFilePath => $"{Application.persistentDataPath}/save{CurrentSaveSlot}.dat";
        
        
        public static Dictionary<string, IEntry> SaveData = new();
        
        
        public static void SaveToDisk()
        {
            var dictionary = new JObject();
            var keys = SaveData.Keys.ToArray();
            var values = SaveData.Values.ToArray();
            for (var index = 0; index < SaveData.Count; index++)
            {
                var key = keys[index];
                var value = values[index];
                if (value.TryGetInt(out var i))
                    dictionary.Add(key, i);
                else if (value.TryGetFloat(out var f))
                    dictionary.Add(key, f);
                else if (value.TryGetString(out var s))
                    dictionary.Add(key, s);
            }
            Debug.Log($"JSON value: {dictionary}");

            File.WriteAllTextAsync(SaveFilePath, dictionary.ToString());
            Debug.Log($"Saved settings to file '{SaveFilePath}'");
        }

        public static void Reset()
        {
            SaveData = new();
        }

        public static void LoadFromDisk()
        {
            if (File.Exists(SaveFilePath))
            {
                Reset();
                Debug.Log($"Loading save from file: {SaveFilePath}");
                var text = File.ReadAllText(SaveFilePath);
                var json = JObject.Parse(text);
                foreach (var prop in json.Properties())
                {
                    if (((float?)prop).HasValue)
                        Set(prop.Name, (float)prop);
                    else if (((int?)prop).HasValue)
                        Set(prop.Name, (string)prop);
                    else
                        Set(prop.Name, prop.Value.ToString());
                }
                //BinaryFormatter bf = new();
                //FloatSettings = (SerializableDictionary<string, FloatEntry>) bf.Deserialize(file);
                //file.Close();
                return;
            }

            Debug.LogError($"No save file at path: {SaveFilePath}");
        }

        public static bool Has(string key)
        {
            return SaveData.ContainsKey(key);
        }
        
        private static IEntry Get(string key, IEntry @default = null)
        {
            if (SaveData.TryGetValue(key, out var value))
                return value;

            @default ??= new NullEntry();

            SaveData[key] = @default;
            return @default;
        }
        public static bool TryGetInt(string key, out int value) => Get(key).TryGetInt(out value);

        public static bool TryGetFloat(string key, out float value) => Get(key).TryGetFloat(out value);

        public static bool TryGetString(string key, out string value) => Get(key).TryGetString(out value);


        public static void Set(string key, float value) =>
            SaveData[key] = new FloatEntry { value = value };

        public static void Set(string key, string value) =>
            SaveData[key] = new StringEntry { value = value };

        public static void Set(string key, int value) =>
            SaveData[key] = new IntEntry { value = value };
        
    }
    
    public interface IEntry
    {
        public bool TryGetFloat(out float value)
        {
            value = default;
            return false;
        }
        public bool TryGetString(out string value)
        {
            value = default;
            return false;
        }
        public bool TryGetInt(out int value)
        {
            value = default;
            return false;
        }
    }

    public struct NullEntry : IEntry { }

    public struct FloatEntry : IEntry
    {
        public float value;

        public bool TryGetFloat(out float value)
        {
            value = this.value;
            return true;
        }
    }
    public struct IntEntry : IEntry
    {
        public int value;
        public bool TryGetInt(out int value)
        {
            value = this.value;
            return true;
        }
    }
    public struct StringEntry : IEntry
    {
        public string value;

        public bool TryGetString(out string value)
        {
            value = this.value;
            return true;
        }
    }

}