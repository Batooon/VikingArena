﻿using System;
using Code.Logic.Animations;
using UnityEngine;

namespace Code.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _hitStateHash = Animator.StringToHash("Damage");
        private readonly int _moveStateHash = Animator.StringToHash("Run");
        private readonly int _dieStateHash = Animator.StringToHash("Die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        
        public AnimatorState State { get; private set; }

        private Animator _animator;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayHit() => 
            _animator.SetTrigger(Hit);

        public void PlayDeath() => 
            _animator.SetTrigger(Die);

        public void Move() => 
            _animator.SetBool(IsMoving, true);

        public void StopMoving() => 
            _animator.SetBool(IsMoving, false);

        public void PlayAttack() => 
            _animator.SetTrigger(Attack);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => 
            StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _dieStateHash)
                state = AnimatorState.Died;
            else if (stateHash == _hitStateHash)
                state = AnimatorState.Hurt;
            else if (stateHash == _moveStateHash)
                state = AnimatorState.Run;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}