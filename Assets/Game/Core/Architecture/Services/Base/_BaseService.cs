using Cysharp.Threading.Tasks;

namespace Game.Core.Architecture.Services.Base
{
    public abstract class BaseService
    {
        public abstract UniTask Initialize();
        public virtual void PostInitialize(){}
        public virtual void OnProjectContextCreated(ProjectContext p){}
    }
}