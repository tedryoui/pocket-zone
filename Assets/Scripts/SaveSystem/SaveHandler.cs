using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.SaveSystem
{
    public class SaveHandler : MonoBehaviour
    {
        private const string SaveFileName = "data.dat";
        
        private List<ISavable> GetSavables
        {
            get
            {
                return FindObjectsOfType<MonoBehaviour>()
                    .OfType<ISavableGetter>()
                    .Select(x => x.GetSavable)
                    .ToList();
            }
        }

        private void Start()
        {
            Load();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if(pauseStatus) Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if(!hasFocus) Save();
        }

        private void Save()
        {
            // Collect data into serializable object
            var savables = GetSavables;
            var saveObject = new SaveObject();
            foreach (var savable in savables)
                savable.SaveIntoObject(saveObject);
            
            // Write data into file
            var path = Path.Combine(Application.persistentDataPath, SaveFileName);
            
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                var data = JsonUtility.ToJson(saveObject, true);

                using var fs = new FileStream(path, FileMode.Create);
                using var writer = new StreamWriter(fs);
                writer.Write(data);
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError("An error occured while writing save data!");
#endif
            }
        }

        private void Load()
        {
            // Write data into file
            SaveObject saveObject = null;
            var path = Path.Combine(Application.persistentDataPath, SaveFileName);

            if (!File.Exists(path))
            {
#if UNITY_EDITOR
                Debug.LogError("Save file no exist!");
#endif
                LoadDefaults();
                return;
            }

            var raw = string.Empty;
            try
            {
                using var fs = new FileStream(path, FileMode.Open);
                using var writer = new StreamReader(fs);
                raw = writer.ReadToEnd();

                saveObject = JsonUtility.FromJson<SaveObject>(raw);
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError("An error occured while reading save data!");
#endif
                LoadDefaults();
            }

            // Fill the data into ISavables
            var savables = GetSavables;
            foreach (var savable in savables)
                savable.LoadFromObject(saveObject);
        }

        private void LoadDefaults()
        {
            // Fill the default data into ISavables
            var savables = GetSavables;
            foreach (var savable in savables)
                savable.LoadDefaults();
        }

        public static void DropSave()
        {
            var path = Path.Combine(Application.persistentDataPath, SaveFileName);
            File.Delete(path);
        }
    }
}