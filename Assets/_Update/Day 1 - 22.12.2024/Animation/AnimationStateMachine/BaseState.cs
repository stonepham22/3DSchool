using UnityEngine;

public abstract class BaseState
{
    protected Animator animator;

    public void Initialize(Animator animator)
    {
        this.animator = animator;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
