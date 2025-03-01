using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform coin; 
    private GrabTrigger[] coins;
    

    void Start()
    {
        setCoins();
    }

    void Update()
    {
    }
    
    void IncrementScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }

    void setCoins()
    {
        coins = Resources.FindObjectsOfTypeAll<GrabTrigger>();
        foreach (GrabTrigger pin in coins)
        {
            pin.OnCoinGrab.AddListener(IncrementScore);
        }
    }
}
