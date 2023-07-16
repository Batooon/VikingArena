using UnityEngine;

namespace Code.Hero
{
    public class AnimateAlongPlayer : MonoBehaviour
    {
        private const float MinimalSquaredSpeed = .01f;

        public CharacterController CharacterController;
        public HeroAnimator Animator;

        private void Start()
        {
            HeroEventsBus.Hit += OnPlayerHit;
            HeroEventsBus.Died += OnPlayerDied;
        }

        private void Update()
        {
            if (ShouldMove())
                Animator.Move();
            else
                Animator.StopMoving();
        }

        private void OnPlayerHit() => 
            Animator.PlayHit();

        private void OnPlayerDied() => 
            Animator.PlayDeath();

        private bool ShouldMove() =>
            CharacterController.velocity.sqrMagnitude > MinimalSquaredSpeed;
    }
}