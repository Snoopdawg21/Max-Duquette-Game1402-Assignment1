using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private GameObject playerObject;
    
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject pauseScreen;
    private bool pauseToggle;

    [SerializeField] private bool doubleJumpBought;

    void OnEnable()
    {
        inputManager.OnPauseGame += Pause;
    }

    void OnDisable()
    {
        inputManager.OnPauseGame -= Pause;
    }

    void Pause()
    {
        pauseToggle = !pauseToggle;
        
        pauseScreen.SetActive(pauseToggle);
    }

    public void ResetPlayer()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();

        if (player == null) return;

        player.Death();
        Pause();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void IncreaseScore(int scoreValue) 
    {
        score += scoreValue;
        Debug.Log(score);
    }

    public void PlusSpeed()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();

        if (player == null) return;

        if (score >= 5)
        {
            player.moveSpeed += 2;
            player.acceleration += 5;
            player.deceleration += 5;
            score -= 5;
        }
        else
        {
            Debug.Log($"You're too poor. You need {5-score} more coins to buy this");
        }
    }

    public void JumpBoost()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();

        if (player == null) return;
        
        if (score >= 5)
        {
            player.jumpForce += 2;
            score -= 5;
        }
        else
        {
            Debug.Log($"You're too poor. You need {5-score} more coins to buy this");
        }
    }

    public void AirSpeed()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();
        
        if (player == null) return;
        if (score >= 5)
        {
            player.airAcceleration += 5;
            player.airDeceleration += 5;
            score -= 5;
        }
        else
        {
            Debug.Log($"You're too poor. You need {5-score} more coins to buy this");
        }
    }

    public void DoubleJump()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();
        
        if (player == null) return;
        if (score >= 5 && !doubleJumpBought)
        {
            player.boughtDoubleJump = true;
            doubleJumpBought = true;
            score -= 5;
        }
        else if (doubleJumpBought)
        {
            Debug.Log("You've already bought this.");
        } else
        {
            Debug.Log($"You're too poor. You need {5-score} more coins to buy this");
        }
    }
}
