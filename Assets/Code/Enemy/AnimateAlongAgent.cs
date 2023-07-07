using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyAnimator))]
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float MinimalSquaredSpeed = .001f;

        public NavMeshAgent Agent;
        public EnemyAnimator Animator;

        private void Update()
        {
            if(ShouldMove())
                Animator.Move();
            else
                Animator.StopMoving();
        }

        private bool ShouldMove() => 
            Agent.velocity.sqrMagnitude > MinimalSquaredSpeed && Agent.remainingDistance > Agent.radius;
    }
}