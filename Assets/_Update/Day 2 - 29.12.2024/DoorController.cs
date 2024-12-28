using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Góc mở cửa
    public float closedAngle = 0f; // Góc đóng cửa
    public float openSpeed = 2f; // Tốc độ xoay cửa
    private float targetAngle; // Góc mục tiêu
    private bool isRotating = false; // Trạng thái cửa đang xoay

    private void Start()
    {
        targetAngle = closedAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger with door");
            targetAngle = openAngle;
            if (!isRotating)
                InvokeRepeating("RotateDoor", 0f, Time.deltaTime); // Bắt đầu xoay cửa
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger with door");
            targetAngle = closedAngle;
            if (!isRotating)
                InvokeRepeating("RotateDoor", 0f, Time.deltaTime); // Bắt đầu xoay cửa
        }
    }

    private void RotateDoor()
    {
        isRotating = true;

        // Lấy góc hiện tại
        float currentAngle = Mathf.LerpAngle(transform.localEulerAngles.y, targetAngle, Time.deltaTime * openSpeed);

        // Cập nhật góc xoay của cửa
        UpdateDoorAngleY(currentAngle);

        // Kiểm tra nếu đã đạt góc mục tiêu
        if (Mathf.Abs(currentAngle - targetAngle) < 0.1f)
        {
            UpdateDoorAngleY(targetAngle);
            CancelInvoke("RotateDoor"); // Dừng việc xoay cửa
            isRotating = false;
        }
    }

    private void UpdateDoorAngleY(float angleY)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angleY, transform.localEulerAngles.z);
    }
}