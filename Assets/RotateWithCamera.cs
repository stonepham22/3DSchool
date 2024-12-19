using UnityEngine;

public class RotateWithCamera : MonoBehaviour
{
    public Transform cameraTransform; // Tham chiếu đến camera

    void Start()
    {
        // Gán camera chính nếu chưa gán
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        RotateCharacterWithCamera();
    }

    void RotateCharacterWithCamera()
    {
        // Lấy hướng nhìn của camera theo trục XZ (loại bỏ trục Y)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;

        // Đảm bảo nhân vật xoay theo hướng camera
        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}