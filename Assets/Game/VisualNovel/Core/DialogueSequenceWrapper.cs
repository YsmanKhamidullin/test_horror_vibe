using System;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture;
using Game.Core.Architecture.Services;
using Game.Core.Canvas;
using Game.Core.Canvas.Base;
using Game.Core.Npc;
using Game.VisualNovel.Core.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.VisualNovel.Core
{
    public class DialogueSequenceWrapper : MonoBehaviour
    {
        private CanvasWindow _canvasWindow;
        
        public async UniTask StartSequence(DialogueSequence dialogueSequence, Npc npc = null, bool isSkipFade = true)
        {
            _canvasWindow = Project.ProjectContext.PlayerController.CanvasWindow;
            
            Debug.Log("Dialog");
            var instance = InstantiateDefault(dialogueSequence);
            instance.OneAlpha();
            instance.HideAll();

            await instance.Play();
        
            instance.gameObject.SetActive(false);
            Object.Destroy(instance.gameObject);
        }

        private async UniTask StartSequence(string path)
        {
            var prefab = Resources.Load<DialogueSequence>(path);
            var instance = InstantiateDefault(prefab);
            await instance.Play();
            Object.Destroy(instance.gameObject);
        }

        private DialogueSequence InstantiateDefault(DialogueSequence prefab)
        {
            var instance = Object.Instantiate(prefab, _canvasWindow.VisualNovelParent);
            return instance;
        }
    }
}