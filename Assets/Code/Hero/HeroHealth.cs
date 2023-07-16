using System;
using Code.Logic;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, IHealth
    {
        public event Action HealthChanged;

        private float _current;

        public float Current
        {
            get => _current;
            set
            {
                if (Mathf.Abs(_current - value) > Constants.Epsilon)
                {
                    _current = value;
                    HeroEventsBus.FireHealthChanged(this);
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max { get; set; }

        public void TakeDamage(float damage)
        {
            if (Current <= 0)
                return;

            Current -= damage;
            HeroEventsBus.FireGotHit();
        }
    }
}