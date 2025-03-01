using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cam; // Assign the Cinemachine Camera in the Inspector
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private LayerMask groundLayer; // Assign this to "Ground" layer in Inspector

    private bool onGround;

    private Rigidbody rb;

    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
        inputManager.OnSpacePressed.AddListener(JumpPlayer);
    }

    private void MovePlayer(Vector2 direction)
    {
        if (direction.magnitude < 0.1f) return; // Prevents small unwanted movement

        // Convert input into world space movement relative to the camera
        Vector3 moveDirection = cam.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        moveDirection.y = 0; // Prevent vertical movement from camera tilt
        moveDirection.Normalize(); // Prevent diagonal speed boost

        rb.AddForce(speed * moveDirection, ForceMode.Acceleration); // Use Acceleration for smoother movement
    }

    private void JumpPlayer()
    {
        if (onGround)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            onGround = false;
        }
    }

    // Adds gravity to make the jump and fall more realistic
    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * 5 * rb.mass);
        onGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }

    
}
