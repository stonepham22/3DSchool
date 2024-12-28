using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Góc mở cửa
    public float closedAngle = 0f; // Góc đóng cửa
    public float openSpeed = 2f; // Tốc độ xoay cửa
    private float targetAngle; // Góc mục tiêu
    private bool isRotating = false; // Trạng thái cửa đang xoay
    private bool isFullyOpened = false; // Trạng thái cửa đã mở hoàn toàn
    private bool playerExited = false; // Trạng thái người chơi đã rời khỏi vùng kích hoạt

    private void Start()
    {
        targetAngle = closedAngle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger with door");

            // Xác định hướng của người chơi so với cửa
            Vector3 directionToPlayer = other.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            if (dotProduct > 0)
            {
                targetAngle = -openAngle; // Mở cửa theo hướng ngược lại
            }
            else
            {
                targetAngle = openAngle; // Mở cửa theo hướng này
            }

            playerExited = false; // Đặt lại trạng thái người chơi đã rời khỏi vùng kích hoạt

            if (!isRotating)
                InvokeRepeating("RotateDoor", 0f, Time.deltaTime); // Bắt đầu xoay cửa
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger with door");
            playerExited = true; // Đánh dấu người chơi đã rời khỏi vùng kích hoạt

            // Nếu cửa đã mở hoàn toàn, bắt đầu đóng cửa
            if (isFullyOpened && !isRotating)
            {
                targetAngle = closedAngle;
                InvokeRepeating("RotateDoor", 0f, Time.deltaTime); // Bắt đầu xoay cửa để đóng lại
            }
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

            // Đặt cờ khi cửa đã mở hoàn toàn
            if (targetAngle == openAngle || targetAngle == -openAngle)
            {
                isFullyOpened = true;

                // Nếu người chơi đã rời khỏi vùng kích hoạt, bắt đầu đóng cửa
                if (playerExited)
                {
                    targetAngle = closedAngle;
                    InvokeRepeating("RotateDoor", 0f, Time.deltaTime); // Bắt đầu xoay cửa để đóng lại
                }
            }
            else if (targetAngle == closedAngle)
            {
                isFullyOpened = false;
            }
        }
    }

    private void UpdateDoorAngleY(float angleY)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angleY, transform.localEulerAngles.z);
    }
}