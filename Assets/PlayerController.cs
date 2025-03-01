using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform cam; 
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float dashCoolDownTime;
    private int jumpCount;
    private bool onGround;
    private bool dashReady = true;

    private Rigidbody rb;

    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
        inputManager.OnSpacePressed.AddListener(JumpPlayer);
        inputManager.OnShiftPressed.AddListener(Dash);
    }

    private void MovePlayer(Vector2 direction)
    {
        if (direction.magnitude < 0.1f) return;

        Vector3 moveDirection = cam.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        moveDirection.y = 0; 
        moveDirection.Normalize(); 

        rb.AddForce(speed * moveDirection, ForceMode.Acceleration); 
    }

    private void JumpPlayer()
    {
        if (onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            jumpCount++;
        }
        else if (jumpCount < 1)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    private void Dash()
    {   
        if (dashReady) {
            rb.AddForce(cam.forward * force, ForceMode.Impulse);
            dashReady = false;
            StartCoroutine(DashCooldown()); 
        }

    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * 5 * rb.mass);
        onGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
        if (onGround) {
            jumpCount = 0;
        }

        Vector3 forwardDirection = cam.forward;
        forwardDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCoolDownTime);
        dashReady = true;
        Debug.Log("Ready to dash!");
    }


}
