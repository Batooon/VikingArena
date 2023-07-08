using Code.UI.Services.Factory;

namespace Code.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        MonsterStaticData ForMonster(MonsterTypeId typeId);
        PlayerStaticData GetPlayerData();
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId windowId);
    }
}