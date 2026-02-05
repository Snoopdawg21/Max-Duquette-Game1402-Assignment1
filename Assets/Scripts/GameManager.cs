using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private GameObject playerObject;

    [SerializeField] private GameObject tutorialScreen;
    private bool tutorialToggle;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI timerText;
    
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject pauseScreen;
    private bool pauseToggle;

    public int minutes;
    private string minuteToggle;
    public float timer;
    private string secondToggle;

    [SerializeField] private bool doubleJumpBought;

    void OnEnable()
    {
        inputManager.OnPauseGame += Pause;
    }

    void OnDisable()
    {
        inputManager.OnPauseGame -= Pause;
    }

    void Start()
    {
        PlayerControler player = playerObject.GetComponent<PlayerControler>();

        if (player == null) return;
        
        scoreText.text = $"${score}";
        healthText.text = $"Health: {player.health}";
    }

    void Update()
    {
        if (minutes < 10)
            minuteToggle = "0";
        else
            minuteToggle = "";
        
        if (timer < 10)
            secondToggle = "0";
        else
            secondToggle = "";

        float seconds = Mathf.Floor(timer);

        timerText.text = $"{minuteToggle}{minutes}:{secondToggle}{seconds}";
        timer += Time.deltaTime;

        if (timer >= 60)
        {
            minutes++;
            timer = 0;
        }
    }

    void Pause()
    {
        pauseToggle = !pauseToggle;
        
        pauseScreen.SetActive(pauseToggle);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LearnGame()
    {
        tutorialToggle = !tutorialToggle;
        
        tutorialScreen.SetActive(tutorialToggle);
    }

    public void HealthUpdate(int health)
    {
        healthText.text = $"Health: {health}";
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
        scoreText.text = $"${score}";
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
            scoreText.text = $"${score}";
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
            scoreText.text = $"${score}";
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
            scoreText.text = $"${score}";
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
            scoreText.text = $"${score}";
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
