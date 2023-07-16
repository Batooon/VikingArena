using UnityEngine;

namespace Code.Infrastructure
{
    public static class CursorUtility
    {
        public static void ShowAndUnlock()
        {
            SetCursorState(true);
        }
        
        public static void HideAndLock()
        {
            SetCursorState(false, CursorLockMode.Locked);
        }

        private static void SetCursorState(bool active, CursorLockMode lockMode = CursorLockMode.None)
        {
            Cursor.visible = active;
            Cursor.lockState = lockMode;
        }
    }
}