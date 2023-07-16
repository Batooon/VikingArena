using Code.Hero;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            CursorUtility.HideAndLock();
            HeroEventsBus.Died += OnPlayerDied;
        }

        public void Exit()
        {
            CursorUtility.ShowAndUnlock();
        }

        private void OnPlayerDied()
        {
            HeroEventsBus.Died -= OnPlayerDied;
            _stateMachine.Enter<GameOverState>();
        }
    }
}