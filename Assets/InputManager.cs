using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent onShiftPressed = new UnityEvent();
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input += Vector2.up;
            Debug.Log($"Key Pressed: { input}");
        }
        if (Input.GetKey(KeyCode.A))
        {
            input += Vector2.left;
            Debug.Log($"Key Pressed: {input}");

        }
        if (Input.GetKey(KeyCode.S))
        {
            input += Vector2.down;
            Debug.Log($"Key Pressed: {input}");

        }
        if (Input.GetKey(KeyCode.D))
        {
            input += Vector2.right;
            Debug.Log($"Key Pressed: {input}");

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
            Debug.Log($"Key Pressed: {input}");

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnSpacePressed?.Invoke();
            Debug.Log($"Key Pressed: {input}");

        }
        OnMove?.Invoke(input);
    }
}
