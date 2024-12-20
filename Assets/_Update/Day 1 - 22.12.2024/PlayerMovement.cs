using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float MoveSpeed = 5f;
    public float SprintSpeed = 20f;
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
        Move();
        Jump();
    }

    void Move()
    {
        float targetSpeed = CalculateTargetSpeed();
        RotateCharacter();
        ApplyMovement(targetSpeed);
    }

    private float CalculateTargetSpeed()
    {
        float targetSpeed = InputManager.Instance.IsSprint ? SprintSpeed : MoveSpeed;
        if (InputManager.Instance.Horizontal == 0 && InputManager.Instance.Vertical == 0)
        {
            targetSpeed = 0.0f; 
        }
        return targetSpeed;
    }

    private void RotateCharacter()
    {
        Vector3 inputDirection = new Vector3(InputManager.Instance.Horizontal, 0.0f, 
                                                InputManager.Instance.Vertical).normalized;
        if (InputManager.Instance.Horizontal != 0 && InputManager.Instance.Vertical != 0)
        {
            TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              MainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref RotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f); 
        }
    }

    private void ApplyMovement(float targetSpeed)
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward;

        CharacterCtrl.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime) +
                         new Vector3(0.0f, Gravity, 0.0f) * Time.deltaTime);

        // _verticalVelocity 
    }

    void Jump()
    {
        CharacterCtrl.Move(new Vector3(0, Gravity, 0) * Time.deltaTime +
                            new Vector3(0, InputManager.Instance.Jump * Time.deltaTime * JumpForce, 0));
    }

}
