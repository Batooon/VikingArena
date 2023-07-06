using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ExitButton : MonoBehaviour
    {
        public Button Button;

        private void Awake() => 
            Button.onClick.AddListener(Quit);

        private void Quit() => 
            Application.Quit();
    }
}