using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    public override void Enter() 
    {
        animator.Play("Run");
    }
    public override void Update() { }
    public override void Exit() { }
}
