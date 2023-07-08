using System;
using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        public EnemyAnimator Animator;

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
                    HealthChanged?.Invoke();
                }
            }
        }

        private float _max;

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;

            Animator.PlayHit();

            HealthChanged?.Invoke();
        }
    }
}