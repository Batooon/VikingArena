using UnityEngine;

namespace Code.Logic
{
    public class Cooldown : MonoBehaviour
    {
        public float Amount;
        public bool Ended = true;

        private float _currentCooldown;

        private void Update()
        {
            if (IsUp() == false)
                _currentCooldown -= Time.deltaTime;
        }

        public void Restart()
        {
            _currentCooldown = Amount;
            Ended = false;
        }

        private bool IsUp()
        {
            bool result = _currentCooldown <= 0;
            if (result && !Ended)
                Ended = true;

            return result;
        }
    }
}