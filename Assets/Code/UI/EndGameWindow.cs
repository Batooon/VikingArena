using Code.Infrastructure.Services.Progress;
using Code.Infrastructure.States;
using Code.UI.Services.Factory;
using UnityEngine.UI;

namespace Code.UI
{
    public class EndGameWindow : WindowBase
    {
        public ScoreCounter ScoreCounter;
        public string PlaySceneName = "Main";
        public Button RestartGameButton;

        public override void Construct(GameStateMachine stateMachine, IProgressService progressService)
        {
            base.Construct(stateMachine, progressService);
            ScoreCounter.Construct(Progress.WorldData);
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            RestartGameButton.onClick.AddListener(RestartGame);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            RestartGameButton.onClick.RemoveListener(RestartGame);
        }

        private void RestartGame()
        {
            ProgressService.Progress = new PlayerProgress();
            StateMachine.Enter<LoadLevelState, string>(PlaySceneName);
        }
    }
}