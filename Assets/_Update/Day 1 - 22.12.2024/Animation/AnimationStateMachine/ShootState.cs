using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : BaseState
{

    public override void Enter()
    {
        animator.Play("Shoot");
    }
    public override void Update() { }
    public override void Exit() { }
}
