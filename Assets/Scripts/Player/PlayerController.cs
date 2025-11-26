using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintMultiplier = 1.6f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    public float verticalClamp = 80f; // prevent full flip

    private float rotationX = 0f;

    void FixedUpdate()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal"); // A - D
        float z = Input.GetAxis("Vertical");   // W - S

        Vector3 moveDir = transform.right * x + transform.forward * z;

        bool sprint = Input.GetKey(KeyCode.LeftShift);

        float finalSpeed = sprint ? moveSpeed * sprintMultiplier : moveSpeed;

        transform.position += moveDir * finalSpeed * Time.fixedDeltaTime;
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        // Rotate horizontal (player body)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate vertical (camera pitch)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalClamp, verticalClamp);

        transform.localRotation = Quaternion.Euler(rotationX, transform.eulerAngles.y, 0f);
    }
}
