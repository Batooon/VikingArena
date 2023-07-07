using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalSquaredDistance = 1f;

        public NavMeshAgent Agent;

        private Transform _playerTransform;

        public void Construct(Transform playerTransform) =>
            _playerTransform = playerTransform;

        private void Update()
        {
            if (HeroNotReached())
                SetDestinationForAgent();
        }

        private bool HeroNotReached() =>
            (_playerTransform.position - Agent.transform.position).sqrMagnitude >= MinimalSquaredDistance;

        private void SetDestinationForAgent()
        {
            if (_playerTransform)
                Agent.destination = _playerTransform.position;
        }
    }
}