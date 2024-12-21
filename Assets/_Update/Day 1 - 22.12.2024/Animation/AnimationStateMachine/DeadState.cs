using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{

    public override void Enter()
    {
        animator.Play("Dead");
    }
    public override void Update() { }
    public override void Exit() { }
}
