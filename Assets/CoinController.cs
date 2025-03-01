using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float rotationSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed *  Time.deltaTime);

    }
}
