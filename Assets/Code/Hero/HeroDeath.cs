using System;
using Code.UI.Services.Factory;
using Code.UI.Services.Windows;
using UnityEngine;

namespace Code.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth Health;
        public HeroMove Move;
        public HeroAttack Attack;
        public HeroAnimator Animator;

        public event Action Happened;

        private bool _isDead;

        private IWindowService _windowService;

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;

        private void Start()
        {
            Health.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (ShouldDie())
                Die();
        }

        private void Die()
        {
            _isDead = true;

            Move.enabled = false;
            Attack.enabled = false;
            Animator.PlayDeath();
            
            _windowService.Open(WindowId.EndGameMenu);
            
            Happened?.Invoke();
        }

        private bool ShouldDie() => 
            _isDead == false && Health.Current <= 0;
    }
}