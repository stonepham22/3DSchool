using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelBlinkState : BaseState
{

    public override void Enter()
    {
        animator.Play("Idle Blink");
    }
    public override void Update() { }
    public override void Exit() { }
}
