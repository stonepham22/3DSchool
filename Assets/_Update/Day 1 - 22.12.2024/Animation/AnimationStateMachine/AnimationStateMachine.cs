using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationStateMachine : StateMachine
{
    private Animator _animator;
    private Dictionary<Type, BaseState> _states;

    public AnimationStateMachine(Animator animator)
    {
        _animator = animator;
        _states = new Dictionary<Type, BaseState>();
        Initialize(IdleState);
    }

    private T GetState<T>() where T : BaseState, new()
    {
        if (!_states.ContainsKey(typeof(T)))
        {
            var state = new T();
            state.Initialize(_animator);
            _states[typeof(T)] = state;
        }
        return (T)_states[typeof(T)];
    }

    public IdleState IdleState => GetState<IdleState>();
    public RunState RunState => GetState<RunState>();
    public WalkState WalkState => GetState<WalkState>();
    public JumpState JumpState => GetState<JumpState>();
    public DeadState DeadState => GetState<DeadState>();
    public IdelBlinkState IdelBlinkState => GetState<IdelBlinkState>();
    public ShootState ShootState => GetState<ShootState>();
}
