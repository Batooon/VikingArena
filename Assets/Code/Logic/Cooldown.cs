using System;
using UnityEngine;

namespace Code.Logic
{
    public class Cooldown : MonoBehaviour
    {
        public float Amount;
        public bool Ended = true;

        public event Action CooldownFinished;

        private float _currentCooldown;

        private bool _initialEndedState;

        private void Awake()
        {
            _initialEndedState = Ended;
        }

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
            {
                Ended = true;
                CooldownFinished?.Invoke();
            }

            return result;
        }

        public void Reset()
        {
            Ended = _initialEndedState;
            _currentCooldown = Amount;
        }
    }
}