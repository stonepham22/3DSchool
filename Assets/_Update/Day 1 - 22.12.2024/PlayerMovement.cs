using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MyGameObjBehaviour
{
    [Tooltip("Handles movement and collision for the player.")]
    [SerializeField] private CharacterController _controller;

    [Tooltip("Reference to the main camera used for player orientation.")]
    [SerializeField] private Camera _mainCamera;
    
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float _moveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    [SerializeField] private float _sprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    [SerializeField] private float _rotationSmoothTime = 0.12f;

    [Tooltip("The current speed of the player.")]
    [SerializeField] private float _speed;

    [Tooltip("The target rotation angle for the player, based on input.")]
    private float _targetRotation;

    [Tooltip("The current rotation velocity used for smooth turning.")]
    private float _rotationVelocity;

    protected override void LoadComponents()
    {
        MyGetReference.Get<CharacterController>(transform, ref _controller);
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // Calculate speed based on input
        float targetSpeed = CalculateTargetSpeed();

        // Smoothly update the player's current speed
        UpdateSpeed(targetSpeed);

        // Get the input direction
        Vector3 inputDirection = GetInputDirection();

        // Rotate the player based on camera direction
        RotatePlayer(inputDirection);

        // Move the player
        MovePlayer();
    }

    /// <summary>
    /// Calculates the target speed based on player input and sprint status.
    /// </summary>
    private float CalculateTargetSpeed()
    {
        // Get movement input from the InputManager
        Vector2 input = new Vector2(InputManager.Ins.Horizontal, InputManager.Ins.Vertical);

        // If there's no input, set target speed to 0
        if (input == Vector2.zero) return 0.0f;

        // Return sprint speed if sprinting, otherwise return normal move speed
        return InputManager.Ins.IsSprint ? _sprintSpeed : _moveSpeed;
    }

    /// <summary>
    /// Smoothly interpolates the current speed towards the target speed.
    /// </summary>
    private void UpdateSpeed(float targetSpeed)
    {
        _speed = Mathf.Lerp(_speed, targetSpeed, Time.deltaTime * 10.0f);
    }

    /// <summary>
    /// Gets the player's normalized input direction.
    /// </summary>
    private Vector3 GetInputDirection()
    {
        Vector2 input = new Vector2(InputManager.Ins.Horizontal, InputManager.Ins.Vertical);
        return new Vector3(input.x, 0.0f, input.y).normalized;
    }

    /// <summary>
    /// Rotates the player to face the input direction relative to the camera.
    /// </summary>
    private void RotatePlayer(Vector3 inputDirection)
    {
        // Skip rotation if there's no input
        if (inputDirection == Vector3.zero) return;

        // Calculate the target rotation based on input and camera direction
        _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                          _mainCamera.transform.eulerAngles.y;

        // Smoothly interpolate to the target rotation
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

        // Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }


    /// <summary>
    /// Moves the player in the direction they are facing.
    /// </summary>
    private void MovePlayer()
    {
        Vector3 moveDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(moveDirection.normalized * (_speed * Time.deltaTime));
    }


}
