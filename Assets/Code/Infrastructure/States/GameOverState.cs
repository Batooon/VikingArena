namespace Code.Infrastructure.States
{
    public class GameOverState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public GameOverState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}