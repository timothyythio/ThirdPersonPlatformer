using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cam; 
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private LayerMask groundLayer;
    private int jumpCount;
    private bool onObstacle;
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
        if (direction.magnitude < 0.1f || (onObstacle && !onGround)) return;

        Vector3 moveDirection = cam.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        moveDirection.y = 0; 
        moveDirection.Normalize(); 

        rb.AddForce(speed * moveDirection, ForceMode.Acceleration); 
    }

    private void JumpPlayer()
    {
        if (onGround)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            onGround = false;
            jumpCount++;
        }
        else if (jumpCount < 1)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * 5 * rb.mass);
        onGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
        if (onGround) {
            jumpCount = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Obstacle"))
        {
            onObstacle = true; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onObstacle = false;
        }
    }


}
