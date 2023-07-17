using System;
using System.Collections.Generic;
using Code.Logic.Animations;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int XVelocity = Animator.StringToHash("x");
        private static readonly int YVelocity = Animator.StringToHash("y");
        private static readonly int Run = Animator.StringToHash("Run");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _runStateHash = Animator.StringToHash("Run");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _deathStateHash = Animator.StringToHash("Die");
        private readonly int _hitStateHash = Animator.StringToHash("Damage");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public bool IsAttacking => StatePerLayer[1] == AnimatorState.Attack;

        public Dictionary<int, AnimatorState> StatePerLayer { get; } = new()
        {
            [0] = AnimatorState.Unknown,
            [1] = AnimatorState.Unknown,
            [2] = AnimatorState.Unknown,
        };

        private Animator _animator;

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayHit() =>
            _animator.SetTrigger(Hit);

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        public void StartRunning() => 
            _animator.SetFloat(Run, 1f);

        public void StopRunning() => 
            _animator.SetFloat(Run, 0f);

        public void PlayAttack() =>
            _animator.SetTrigger(Attack);

        public void SetVelocity(Vector2 velocity)
        {
            _animator.SetFloat(XVelocity, velocity.x);
            _animator.SetFloat(YVelocity, velocity.y);
        }

        public void EnteredState(int stateHash, int layerId)
        {
            StatePerLayer[layerId] = StateFor(stateHash);
            StateEntered?.Invoke(StatePerLayer[layerId]);
        }

        public void ExitedState(int stateHash, int layerId)
        {
            var exitedState = StateFor(stateHash);

            StateExited?.Invoke(exitedState);
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else if (stateHash == _runStateHash)
                state = AnimatorState.Run;
            else if (stateHash == _hitStateHash)
                state = AnimatorState.Hurt;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}