using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Góc mở cửa
    public float closedAngle = 0f; // Góc đóng cửa
    public float openSpeed = 2f; // Tốc độ xoay cửa
    private bool isRotating = false; // Trạng thái cửa đang xoay
    private float targetOpen; // Góc mục tiêu khi mở cửa

    private void Start()
    {
        // Đặt góc ban đầu của cửa là đóng
        UpdateDoorAngleY(closedAngle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke("CloseDoor"); // Hủy việc đóng cửa nếu đang thực hiện

            // Xác định hướng của người chơi so với cửa
            Vector3 directionToPlayer = other.transform.forward;
            float dotProduct = Vector3.Dot(transform.forward, directionToPlayer);

            if (dotProduct > 0)
            {
                targetOpen = openAngle; // Mở cửa theo hướng ngược lại
            }
            else
            {
                targetOpen = -openAngle; // Mở cửa theo hướng này
            }

            InvokeRepeating("OpenDoor", 0f, 0.01f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke("OpenDoor"); // Hủy việc mở cửa nếu đang thực hiện
            InvokeRepeating("CloseDoor", 0f, 0.01f);
        }
    }

    private void OpenDoor()
    {
        isRotating = true;

        // Lấy góc hiện tại
        float currentAngle = Mathf.LerpAngle(transform.localEulerAngles.y, targetOpen, Time.deltaTime * openSpeed);

        // Cập nhật góc xoay của cửa
        UpdateDoorAngleY(currentAngle);

        // Kiểm tra nếu đã đạt góc mở mục tiêu
        if (Mathf.Abs(currentAngle - targetOpen) < 0.1f)
        {
            UpdateDoorAngleY(targetOpen);
            CancelInvoke("OpenDoor"); // Dừng việc xoay cửa khi mở
            isRotating = false;
        }
    }

    private void CloseDoor()
    {
        isRotating = true;

        // Lấy góc hiện tại
        float currentAngle = Mathf.LerpAngle(transform.localEulerAngles.y, closedAngle, Time.deltaTime * openSpeed);

        // Cập nhật góc xoay của cửa
        UpdateDoorAngleY(currentAngle);

        // Kiểm tra nếu đã đạt góc đóng mục tiêu
        if (Mathf.Abs(currentAngle - closedAngle) < 0.1f)
        {
            UpdateDoorAngleY(closedAngle);
            CancelInvoke("CloseDoor"); // Dừng việc xoay cửa khi đóng
            isRotating = false;
        }
    }

    private void UpdateDoorAngleY(float angleY)
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angleY, transform.localEulerAngles.z);
    }
}