using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintMultiplier = 1.6f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 200f;
    public float verticalClamp = 80f;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();   // Get controller
        
    }

    void Update()
    {
        MovePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Movement without Y axis
        Vector3 move = transform.right * x + transform.forward * z;
        move.y = 0; // Prevent vertical movement

        bool sprint = Input.GetKey(KeyCode.LeftShift);
        float speed = sprint ? moveSpeed * sprintMultiplier : moveSpeed;

        controller.Move(move * speed * Time.deltaTime);
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(1)) // Rotate only when holding right click
        {
            rotationY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            rotationX = Mathf.Clamp(rotationX, -verticalClamp, verticalClamp);

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f); // FULL X+Y rotation here
        }
    }
}
