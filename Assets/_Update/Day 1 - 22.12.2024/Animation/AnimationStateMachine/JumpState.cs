public class JumpState : BaseState
{

    public override void Enter()
    {
        animator.Play("Jump");
    }
    public override void Update() { }
    public override void Exit() { }
}