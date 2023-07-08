using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
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

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }
    }
}