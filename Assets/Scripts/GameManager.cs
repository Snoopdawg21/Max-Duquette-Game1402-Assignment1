using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private TMP_Text healthText;
    
    void Start()
    {
        DisplayHealth();
        
    }

    public void IncreaseScore(int scoreValue) 
    {
        score += scoreValue;
        Debug.Log(score);
    }

    public void DisplayHealth()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();

        healthText.text = $"Health: {player.health}";
    }
}
