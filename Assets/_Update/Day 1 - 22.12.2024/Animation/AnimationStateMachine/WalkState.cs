using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{

    public override void Enter() 
    {
        animator.Play("Walk");
    }
    public override void Update() { }
    public override void Exit() { }
}
