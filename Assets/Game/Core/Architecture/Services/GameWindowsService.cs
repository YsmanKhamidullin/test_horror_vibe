using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core.Architecture.Services.Base;
using Game.Core.Canvas;
using Game.Core.Canvas.Base;

namespace Game.Core.Architecture.Services
{
    public class GameWindowsService : BaseService
    {
        private List<BaseWindow> Windows = new List<BaseWindow>();


        public override async UniTask Initialize()
        {
            await UniTask.CompletedTask;
        }

        public override void PostInitialize()
        {
        }

        public void Register(BaseWindow w)
        {
            Windows.Add(w);
        }

        public T Get<T>() where T : BaseWindow
        {
            foreach (var w in Windows)
            {
                if (w is T window)
                {
                    return window;
                }
            }

            return null;
        }

        public void Debug()
        {
            foreach (var w in Windows)
            {
                UnityEngine.Debug.Log(w.gameObject.name);
            }
        }
    }
}