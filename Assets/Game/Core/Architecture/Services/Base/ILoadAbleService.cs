namespace Game.Core.Architecture.Services.Base
{
    public interface ILoadAbleService
    {
        public void LoadFromSave(SaveData saveData);
    }
}