using System;
using Code.Infrastructure.Services.Progress;
using Code.Logic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth), typeof(EnemyAttack))]
    public class EnemyDeath : MonoBehaviour
    {
        public int ScoreAmount;
        public EnemyHealth Health;
        public EnemyAttack Attack;
        public EnemyAnimator Animator;
        public Follow Follow;
        public Cooldown DestroyTimer;

        public event Action<GameObject> Happened;

        private WorldData _worldData;
        private bool _died;

        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }

        private void Start() =>
            Health.HealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            Health.HealthChanged -= OnHealthChanged;

        public void Restore() => 
            _died = false;

        private void OnHealthChanged()
        {
            if (ShouldDie())
                Die();
        }

        [UsedImplicitly]
        private void OnDeathAnimationEnded()
        {
            DestroyTimer.CooldownFinished += OnDestroyCooldown;
            DestroyTimer.Restart();
        }

        private void Die()
        {
            _died = true;
            Follow.enabled = false;
            Attack.enabled = false;
            _worldData.Score.Increase(ScoreAmount);
            Animator.PlayDeath();
        }

        private bool ShouldDie() => 
            !_died && Health.Current <= 0;

        private void OnDestroyCooldown()
        {
            DestroyTimer.CooldownFinished -= OnDestroyCooldown;
            gameObject.SetActive(false);
            Happened?.Invoke(gameObject);
        }
    }
}