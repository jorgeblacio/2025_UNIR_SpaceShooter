using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game Settings")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private Vector3 respawnPosition = new Vector3(-6, 0, 0);
    
    [Header("References")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private InputActionAsset spaceShooterActionsAsset;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EnemySpawner enemySpawner;
    
    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    
    private int currentLives;
    private GameObject currentPlayer;
    private bool isGameActive = false;
    
    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }
    
    private GameState currentState = GameState.MainMenu;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        ShowMainMenu();
    }
    
    public void ShowMainMenu()
    {
        currentState = GameState.MainMenu;
        Time.timeScale = 0;
        
        if (uiManager != null)
        {
            uiManager.ShowMainMenu();
        }
        
        if (musicSource != null && menuMusic != null)
        {
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    
    public void StartGame()
    {
        currentState = GameState.Playing;
        currentLives = startingLives;
        Time.timeScale = 1;
        
        if (uiManager != null)
        {
            uiManager.ShowGameUI();
            uiManager.UpdateLives(currentLives);
        }
        if (musicSource != null && gameMusic != null)
        {
            musicSource.clip = gameMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        
        SpawnPlayer();
        
        if (enemySpawner != null)
        {
            enemySpawner.StartSpawning();
        }
        
        isGameActive = true;
    }
    
    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPosition, Quaternion.identity);
            
            PlayerSpaceShip player = currentPlayer.GetComponent<PlayerSpaceShip>();
            
            if (player != null && spaceShooterActionsAsset != null)
            {
                InputActionMap spaceShipActionMap = spaceShooterActionsAsset.FindActionMap("SpaceShip");

                if (spaceShipActionMap != null)
                {
                    InputAction moveAction = spaceShipActionMap.FindAction("move");
                    InputAction shootAction = spaceShipActionMap.FindAction("shoot");

                    if (moveAction != null)
                    {
                        player.move = InputActionReference.Create(moveAction);
                    }

                    if (shootAction != null)
                    {
                        player.shoot = InputActionReference.Create(shootAction);
                    }
                    currentPlayer.SetActive(true);
                }
                else
                {
                    Debug.LogError("Action Map 'SpaceShip' not found in the input asset.");
                }
                player.OnPlayerDeath += HandlePlayerDeath;
            } 
            else if (player == null)
            {
                Debug.LogError("Player prefab is missing the PlayerSpaceShip component.");
            }
            else if (spaceShooterActionsAsset == null)
            {
                Debug.LogError("Space Shooter Actions Asset is not assigned in the GameManager!");
            }
        }
    }

    public void HandleWeaponPowerUpPickup()
    {
        if (isGameActive && currentPlayer != null)
        {
            PlayerSpaceShip spaceShip = currentPlayer.GetComponent<PlayerSpaceShip>();
            Debug.Log("Power-up picked up by player.");
            if (spaceShip.weaponManager != null)
            {
                Debug.Log("Powering up the player's weapon.");
                spaceShip.weaponManager.PowerUpCurrentWeapon();
            }
        }
    }
    
    public void HandlePlayerDeath()
    {
        currentLives--;
        
        if (uiManager != null)
        {
            uiManager.UpdateLives(currentLives);
        }
        
        if (currentLives > 0)
        {
            Invoke(nameof(RespawnPlayer), respawnDelay);
        }
        else
        {
            Invoke(nameof(GameOver), respawnDelay);
        }
    }
    
    private void RespawnPlayer()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        SpawnPlayer();
    }
    
    public void GameOver()
    {
        currentState = GameState.GameOver;
        isGameActive = false;
        Time.timeScale = 0; 
        
        if (uiManager != null)
        {
            uiManager.ShowGameOver();
        }
        
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
    
    public void RestartGame()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
        
        if (enemySpawner != null)
        {
            enemySpawner.ResetSpawner();
        }
        
        StartGame();
    }
    
    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    public int GetCurrentLives()
    {
        return currentLives;
    }
    
    public bool IsGameActive()
    {
        return isGameActive;
    }
}