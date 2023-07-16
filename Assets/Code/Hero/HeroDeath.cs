using Code.Infrastructure.Services.StaticData;
using Code.Logic;
using Code.UI.Services.Windows;
using UnityEngine;

namespace Code.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        private bool _isDead;

        private IWindowService _windowService;

        public void Construct(IWindowService windowService) =>
            _windowService = windowService;

        private void Start() => 
            HeroEventsBus.HealthChanged += OnHealthChanged;

        private void OnHealthChanged(IHealth health)
        {
            if (ShouldDie(health))
                Die();
        }

        private void Die()
        {
            _isDead = true;

            HeroEventsBus.FireHeroDiedEvent();
            
            _windowService.Open(WindowId.EndGameMenu);
        }

        private bool ShouldDie(IHealth health) =>
            _isDead == false && health.Current <= 0;
    }
}