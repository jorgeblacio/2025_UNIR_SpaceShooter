using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameUIPanel;
    [SerializeField] private GameObject gameOverPanel;
    
    [Header("Game UI Elements")]
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Image[] lifeIcons;
    
    [Header("Game Over UI")]
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button restartButton;
    
    private void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartGame);
        }
        
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartGame);
        }
    }
    
    public void ShowMainMenu()
    {
        mainMenuPanel?.SetActive(true);
        gameUIPanel?.SetActive(false);
        gameOverPanel?.SetActive(false);
    }
    
    public void ShowGameUI()
    {
        mainMenuPanel?.SetActive(false);
        gameUIPanel?.SetActive(true);
        gameOverPanel?.SetActive(false);
    }
    
    public void ShowGameOver()
    {
        mainMenuPanel?.SetActive(false);
        gameUIPanel?.SetActive(false);
        gameOverPanel?.SetActive(true);
    }
    
    public void UpdateLives(int lives)
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
        
        if (lifeIcons != null && lifeIcons.Length > 0)
        {
            for (int i = 0; i < lifeIcons.Length; i++)
            {
                lifeIcons[i].enabled = i < lives;
            }
        }
    }
    
    private void OnStartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
    }
    
    private void OnRestartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }
    
    private void OnQuitGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
    }
}