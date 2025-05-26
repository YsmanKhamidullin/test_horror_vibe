using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services.Base;
using UnityEngine;

namespace Game.Core.Architecture.Services
{
    public class LoadService : BaseService
    {
        public SaveData CurrentSaveData;
        private string _saveFilePath;

        public override async UniTask Initialize()
        {
            _saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
            await LoadAsync();
            ApplyLoadedDataToServices();
        }

        public async UniTask LoadAsync()
        {
            if (!File.Exists(_saveFilePath))
            {
                CurrentSaveData = CreateNewSave();
                return;
            }

            try
            {
                string json = await File.ReadAllTextAsync(_saveFilePath);
                CurrentSaveData = JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Load failed: {e}. Creating new save data");
                CurrentSaveData = CreateNewSave();
            }
        }

        private void ApplyLoadedDataToServices()
        {
            var loadAbleServices = Project.GetAll<ILoadAbleService>();
            for (int i = 0; i < loadAbleServices.Count; i++)
            {
                loadAbleServices[i].LoadFromSave(CurrentSaveData);
            }
        }

        private SaveData CreateNewSave()
        {
            Debug.Log("Creating new Save Data");
            return new SaveData();
        }
    }
}