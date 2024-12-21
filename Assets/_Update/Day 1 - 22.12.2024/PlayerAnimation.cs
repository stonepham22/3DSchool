using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : BaseAnimation
{
    [SerializeField] private PlayerJump _playerJump;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        MyGetReference.Get<PlayerJump>(transform, ref _playerJump);
    }

    void Update()
    {
        TransitionBaseOnInput();
    }

    public void TransitionBaseOnInput()
    {
        float horizontal = InputManager.Ins.Horizontal;

        float vertical = InputManager.Ins.Vertical;

        bool isSprinting = InputManager.Ins.IsSprint;

        // Check if the player is in the air by calling the IsGrounded() method from PlayerJump.
        if (!GetComponent<PlayerJump>().IsGrounded() && InputManager.Ins.Jump != 0)
        {
            // If the player is airborne, transition to the Jump animation state.
            stateMachine.TransitionTo(stateMachine.JumpState);

            // Exit the method to prevent further animation transitions.
            return;
        }

        // If no input is detected (input magnitude is 0), transition to the Idle animation state.
        // if (inputMagnitude == 0)
        // {
        //     stateMachine.TransitionTo(stateMachine.IdleState);
        // }

        if (horizontal == 0 && vertical == 0)
        {
            stateMachine.TransitionTo(stateMachine.IdleState);
        }

        // If the player is sprinting, transition to the Run animation state.
        else if (isSprinting)
        {
            stateMachine.TransitionTo(stateMachine.RunState);
        }

        // If the player is moving but not sprinting, transition to the Walk animation state.
        else
        {
            stateMachine.TransitionTo(stateMachine.WalkState);
        }
    }


}
