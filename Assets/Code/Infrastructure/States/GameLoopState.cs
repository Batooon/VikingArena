using UnityEngine;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        public GameLoopState(GameStateMachine stateMachine)
        {
        }

        public void Enter()
        {
            HideAndLockCursor();
        }

        public void Exit()
        {
            SetCursorState(true);
        }

        private void HideAndLockCursor()
        {
            SetCursorState(false, CursorLockMode.Locked);
        }

        private void SetCursorState(bool active, CursorLockMode lockMode = CursorLockMode.None)
        {
            Cursor.visible = active;
            Cursor.lockState = lockMode;
        }
    }
}