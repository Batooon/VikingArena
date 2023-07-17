using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class AnimateAlongPlayer : MonoBehaviour
    {
        public HeroAnimator Animator;
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            HeroEventsBus.Hit += OnPlayerHit;
            HeroEventsBus.Died += OnPlayerDied;
        }

        private void Update()
        {
            Animator.SetVelocity(_inputService.Axis);
            
            if (_inputService.IsHoldingRun())
                Animator.StartRunning();
            else
                Animator.StopRunning();
        }

        private void OnPlayerHit() => 
            Animator.PlayHit();

        private void OnPlayerDied() => 
            Animator.PlayDeath();
    }
}