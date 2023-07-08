using Code.Infrastructure.Services.AssetManagement;
using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.States;
using Code.UI.Windows;
using UnityEngine;

namespace Code.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData, IProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void CreateMainMenu(GameStateMachine stateMachine) =>
            CreateMenu(WindowId.MainMenu, stateMachine);

        public void CreateEndGameMenu(GameStateMachine stateMachine) =>
            CreateMenu(WindowId.EndGameMenu, stateMachine);

        private void CreateMenu(WindowId windowId, GameStateMachine stateMachine)
        {
            WindowConfig config = _staticData.ForWindow(windowId);
            WindowBase window = Object.Instantiate(config.Prefab, _uiRoot);

            window.Construct(stateMachine, _progressService);
        }

        public void CreateUIRoot()
        {
            _uiRoot = _assets.Instantiate(AssetPath.UIRootPath).transform;
        }
    }
}