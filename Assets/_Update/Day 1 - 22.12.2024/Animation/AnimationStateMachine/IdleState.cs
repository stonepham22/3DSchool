public class IdleState : BaseState
{

    public override void Enter() 
    {
        animator.Play("Idle");
    }
    public override void Update() { }
    public override void Exit() { }
}
