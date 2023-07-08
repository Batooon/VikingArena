using System;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.AssetManagement;
using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Code.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateMainMenu(GameStateMachine stateMachine);
        void CreateEndGameMenu(GameStateMachine stateMachine);
        void CreateUIRoot();
    }

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

    public class WindowBase : MonoBehaviour
    {
        protected IProgressService ProgressService;
        protected GameStateMachine StateMachine;

        protected PlayerProgress Progress => ProgressService.Progress;

        public Button CloseButton;

        public virtual void Construct(GameStateMachine stateMachine, IProgressService progressService)
        {
            StateMachine = stateMachine;
            ProgressService = progressService;
        }

        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        protected virtual void OnAwake() => 
            CloseButton.onClick.AddListener(() => Application.Quit());

        private void OnDestroy() => 
            Cleanup();

        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }

    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }

    public enum WindowId
    {
        Unknown = 0,
        MainMenu = 1,
        EndGameMenu = 2,
    }
}