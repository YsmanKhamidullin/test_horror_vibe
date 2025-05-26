using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services.Base;
using UnityEngine;

namespace Game.Core.Architecture.Services
{
    [Serializable]
    public class SaveData
    {
    }

    public class SaveService : BaseService
    {
        private const string SaveFileName = "savegame.json";
        private string _saveFilePath;

        public override UniTask Initialize()
        {
            _saveFilePath = Path.Combine(Application.persistentDataPath, SaveFileName);
            return UniTask.CompletedTask;
        }

        public override void PostInitialize()
        {
        }

        public async UniTask SaveAsync()
        {
            SaveData saveData = BuildSaveData();
            try
            {
                string json = JsonUtility.ToJson(saveData, true);
                await File.WriteAllTextAsync(_saveFilePath, json);
                Debug.Log("Game saved successfully");
            }
            catch (Exception e)
            {
                Debug.LogError($"Save failed: {e.Message}");
            }
        }

        private SaveData BuildSaveData()
        {
            SaveData saveData = new SaveData();
            var saveAbleServices = Project.GetAll<ISaveAbleService>();
            foreach (var s in saveAbleServices)
            {
                saveData = s.FillSaveData(saveData);
            }

            return saveData;
        }
    }
}