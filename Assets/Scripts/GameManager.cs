using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private GameObject playerObject;
    
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
        
    }

    public void PlusSpeed()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();

        if (player == null) return;

        player.moveSpeed += 2;
        player.acceleration += 5;
        player.deceleration += 5;
    }
}
