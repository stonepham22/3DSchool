using UnityEngine;
public abstract class BaseAnimation : MyGameObjBehaviour
{
    [Header("Base Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] protected AnimationStateMachine stateMachine;

    protected override void LoadComponents()
    {
        MyGetReference.Get<Animator>(transform, ref _animator);
        stateMachine = new AnimationStateMachine(_animator);
    }

    public void TransitionTo(EnumAnimationState state)
    {
        switch (state)
        {
            case EnumAnimationState.Idle:
                stateMachine.TransitionTo(stateMachine.IdleState);
                break;
            case EnumAnimationState.Run:
                stateMachine.TransitionTo(stateMachine.RunState);
                break;
            case EnumAnimationState.Jump:
                stateMachine.TransitionTo(stateMachine.JumpState);
                break;
            case EnumAnimationState.Dead:
                stateMachine.TransitionTo(stateMachine.DeadState);
                break;
            case EnumAnimationState.Walk:
                stateMachine.TransitionTo(stateMachine.WalkState);
                break;
            case EnumAnimationState.Shoot:
                stateMachine.TransitionTo(stateMachine.ShootState);
                break;
        }
    }

}
