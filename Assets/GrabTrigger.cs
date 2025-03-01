using UnityEngine;
using UnityEngine.Events;

public class GrabTrigger : MonoBehaviour
{
    public UnityEvent OnCoinGrab = new();
    public bool isCoinGrabbed = false;
    void Start()
    {

    }
    private void OnTriggerEnter(Collider triggeredObject)
    {
        if (triggeredObject.CompareTag("Player") && !isCoinGrabbed)
        {
            isCoinGrabbed = true;
            OnCoinGrab?.Invoke();
            Debug.Log($"{gameObject.name} has been grabbed!");
            Destroy(gameObject);
        }

    }
    void Update()
    {

    }
}
