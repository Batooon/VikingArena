using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.Progress;
using Code.Logic;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour
    {
        public HeroAnimator Animator;
        public CharacterController CharacterController;

        private IInputService _inputService;

        private int _layerMask;
        private Collider[] _hits = new Collider[3];
        private Stats _stats;

        public void Construct(IInputService inputService, Stats stats)
        {
            _inputService = inputService;
            _stats = stats;

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Start() => 
            HeroEventsBus.Died += OnHeroDied;

        private void Update()
        {
            if (CanAttack())
                Animator.PlayAttack();
        }

        [UsedImplicitly]
        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
        }

        private bool CanAttack() =>
            _inputService.IsAttackPressed() && Animator.IsAttacking == false;

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.Cleavage, _hits, _layerMask);

        private Vector3 StartPoint() =>
            transform.position + Vector3.up;

        private void OnHeroDied() => 
            enabled = false;
    }
}