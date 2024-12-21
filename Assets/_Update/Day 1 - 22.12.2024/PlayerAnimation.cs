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

        // Check if the player is in the air by calling the IsGrounded() method from PlayerJump.
        if (!_playerJump.IsGrounded() && InputManager.Ins.Jump != 0)
        {
            // If the player is airborne, transition to the Jump animation state.
            stateMachine.TransitionTo(stateMachine.JumpState);

            // Exit the method to prevent further animation transitions.
            return;
        }

        if (InputManager.Ins.Horizontal == 0 && InputManager.Ins.Vertical == 0)
        {
            stateMachine.TransitionTo(stateMachine.IdleState);
        }

        // If the player is sprinting, transition to the Run animation state.
        else if (InputManager.Ins.IsSprint)
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
