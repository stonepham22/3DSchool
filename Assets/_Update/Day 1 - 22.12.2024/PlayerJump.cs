using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MyGameObjBehaviour
{
    [Tooltip("The height the player can jump")]
    [SerializeField] private float _jumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    [SerializeField] private float _gravity = -15.0f;

    [Tooltip("Time required to pass before being able to jump again")]
    [SerializeField] private float _jumpTimeout = 0.50f;

    [Tooltip("Handles player movement and collisions.")]
    [SerializeField] private CharacterController _controller;

    [Tooltip("Current vertical velocity for jump and gravity.")]
    private float _verticalVelocity;

    [Tooltip("Cooldown timer between jumps.")]
    private float _jumpTimeoutDelta;


    protected override void LoadComponents()
    {
        ReferenceManager.Get<CharacterController>(transform, ref _controller);
        _jumpTimeoutDelta = _jumpTimeout;
    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (_controller.isGrounded)
        {
            // Reset the vertical velocity when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump logic
            if (Input.GetButtonDown("Jump") && _jumpTimeoutDelta <= 0.0f)
            {
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            }

            if (_jumpTimeoutDelta > 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // Reset the jump timeout when in the air
            _jumpTimeoutDelta = _jumpTimeout;
        }

        // Apply gravity over time
        _verticalVelocity += _gravity * Time.deltaTime;

        // Apply the movement
        Vector3 verticalMovement = new Vector3(0.0f, _verticalVelocity, 0.0f);
        _controller.Move(verticalMovement * Time.deltaTime);
    }


}
