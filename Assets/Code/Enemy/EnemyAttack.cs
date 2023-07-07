using System.Linq;
using Code.Logic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        public EnemyAnimator Animator;
        public Cooldown Cooldown;
        public float EffectiveDistance = .5f;
        public float Damage = 10f;
        public float Cleavage = .5f;

        private Transform _playerTransform;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];

        private bool _isAttacking;
        private bool _isAttackActive;

        public void Construct(Transform playerTransform)
        {
            _playerTransform = playerTransform;

            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (CanAttack())
                StartAttack();
        }

        private void StartAttack()
        {
            transform.LookAt(_playerTransform);

            Animator.PlayAttack();
            _isAttacking = true;
        }

        [UsedImplicitly]
        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                PhysicsDebug.DrawDebug(StartPoint(), Cleavage, 1f);
                hit.transform.GetComponent<IHealth>().TakeDamage(Damage);
            }
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z) +
                   transform.forward * EffectiveDistance;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private bool CanAttack() =>
            _isAttackActive && !_isAttacking && Cooldown.Ended;
    }
}