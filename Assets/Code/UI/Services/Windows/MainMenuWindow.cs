using Code.Infrastructure.States;
using Code.UI.Services.Factory;
using UnityEngine.UI;

namespace Code.UI.Services.Windows
{
    public class MainMenuWindow : WindowBase
    {
        public string PlaySceneName = "Main";
        public Button StartGameButton;

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            StartGameButton.onClick.AddListener(StartGame);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            StartGameButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame() => 
            StateMachine.Enter<LoadLevelState, string>(PlaySceneName);
    }
}