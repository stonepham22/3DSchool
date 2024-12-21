using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MyGameObjBehaviour
{
    [Tooltip("Handles player movement and collisions.")]
    [SerializeField] private CharacterController _controller;

    [Tooltip("The height the player can jump")]
    [SerializeField] private float _jumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    [SerializeField] private float _gravity = -15.0f;

    [Tooltip("Time required to pass before being able to jump again")]
    [SerializeField] private float _jumpTimeout = 0.50f;

    [Tooltip("Current vertical velocity for jump and gravity.")]
    private float _verticalVelocity;

    [Tooltip("Cooldown timer between jumps.")]
    private float _jumpTimeoutDelta;

    [Tooltip("The layers considered as ground for the player.")]
    [SerializeField] private LayerMask _groundLayers;

    [Tooltip("The distance used to check if the player is grounded.")]
    [SerializeField] private float _groundCheckDistance = 0.2f;


    protected override void LoadComponents()
    {
        ReferenceManager.Get<CharacterController>(transform, ref _controller);
        _jumpTimeoutDelta = _jumpTimeout;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            HandleGroundedLogic();
        }
        else
        {
            //Resets the jump timeout when the player is airborne.
            _jumpTimeoutDelta = _jumpTimeout;
        }

        // Applies gravity to the player's vertical velocity.
        _verticalVelocity += _gravity * Time.deltaTime;

        ApplyVerticalMovement();
    }

    // Handles logic when the player is grounded.
    private void HandleGroundedLogic()
    {
        // Reset vertical velocity when grounded
        if (_verticalVelocity < 0.0f)
        {
            _verticalVelocity = -2f;
        }

        // Handle jump logic if input is detected
        if (InputManager.Ins.Jump != 0 && _jumpTimeoutDelta <= 0.0f)
        {
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        // Decrease the jump timeout
        if (_jumpTimeoutDelta > 0.0f)
        {
            _jumpTimeoutDelta -= Time.deltaTime;
        }
    }

    // Moves the player vertically based on the vertical velocity.
    private void ApplyVerticalMovement()
    {
        Vector3 verticalMovement = new Vector3(0.0f, _verticalVelocity, 0.0f);
        _controller.Move(verticalMovement * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        // 
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance,
                                                                    _groundLayers);
    }


}
