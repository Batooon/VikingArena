using Code.Logic;
using UnityEngine;

namespace Code.Enemy
{
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttack Attack;
        public TriggerObserver TriggerObserver;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;

            Attack.DisableAttack();
        }

        private void TriggerEnter(Collider other) =>
            Attack.EnableAttack();

        private void TriggerExit(Collider other) =>
            Attack.DisableAttack();
    }
}