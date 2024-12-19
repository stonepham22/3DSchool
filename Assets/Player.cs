using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Input")]
    public float Horizontal;
    public float Vertical;
    public float InputJump;
    public bool IsSprint;

    [Header("Move")]
    public float MoveSpeed;
    public float SprintSpeed;
    public float TargetRotation;
    public Camera MainCamera;
    public float RotationVelocity;
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;


    [Header("Jump")]
    public float JumpForce;
    public float Gravity = -9.81f;

    public CharacterController CharacterCtrl;

    void Start()
    {
        CharacterCtrl = GetComponent<CharacterController>();
        MainCamera = Camera.main;
    }

    void Update()
    {
        GetInput();
        Move();
        Jump();
        // 1 nam 1 nu 
        // 
    }

    void Move()
    {
        float targetSpeed = CalculateTargetSpeed();
        RotateCharacter();
        ApplyMovement(targetSpeed);
    }

    private float CalculateTargetSpeed()
    {
        float targetSpeed = IsSprint ? SprintSpeed : MoveSpeed;
        if (Horizontal == 0 && Vertical == 0)
        {
            targetSpeed = 0.0f; // Không di chuyển nếu không có input
        }
        return targetSpeed;
    }

    private void RotateCharacter()
    {
        Vector3 inputDirection = new Vector3(Horizontal, 0.0f, Vertical).normalized;
        if (Horizontal != 0 && Vertical != 0)
        {
            TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              MainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref RotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f); // Xoay nhân vật theo góc
        }
    }

    private void ApplyMovement(float targetSpeed)
    {
        // Vector3 inputDirection = new Vector3(Horizontal, 0.0f, Vertical).normalized;
        Vector3 targetDirection = Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward;

        CharacterCtrl.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime) +
                         new Vector3(0.0f, Gravity, 0.0f) * Time.deltaTime);

        // _verticalVelocity 
    }

    void Jump()
    {
        CharacterCtrl.Move(new Vector3(0, Gravity, 0) * Time.deltaTime +
                            new Vector3(0, InputJump * Time.deltaTime * JumpForce, 0));
    }

    void GetInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        InputJump = Input.GetAxis("Jump");
        IsSprint = Input.GetKey(KeyCode.LeftShift);
    }

}
