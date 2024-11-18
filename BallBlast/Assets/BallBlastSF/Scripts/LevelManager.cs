using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject defeatPanel;
    public GameObject passedPanel;
    public StoneSpawner stoneSpawner;
    public Cart cart;
    //public ProgressPanel progressPanel;

    
    public static int amount = 1; 
    public static float maxHitpointsRate = 1.0f; 
    public static int coins = 0; 
    public static int currentLevel = 1; 

    private int totalStones;
    private int destroyedStones;

    private void Start()
    {
        ShowStartPanel();
        defeatPanel.SetActive(false);
        passedPanel.SetActive(false);
        Time.timeScale = 0f;

        
        stoneSpawner.amount = amount;
        stoneSpawner.maxHitpointsRate = maxHitpointsRate;
        cart.SetCoins(coins);

        
        totalStones = stoneSpawner.amount;
        destroyedStones = 0;
        //progressPanel.InitializeProgressBar(totalStones);
        //progressPanel.UpdateLevelTexts();
    }

    public void StartGame()
    {
        HideStartPanel();
        stoneSpawner.enabled = true;
        Time.timeScale = 1f;
        InvokeRepeating(nameof(CheckForRemainingStones), 0f, 0.5f); 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        
        amount++;
        maxHitpointsRate += 1f;
        currentLevel++;

        
        stoneSpawner.amount = amount;
        stoneSpawner.maxHitpointsRate = maxHitpointsRate;

        
        coins = cart.GetCoins();

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ShowStartPanel()
    {
        startPanel.SetActive(true);
    }

    private void HideStartPanel()
    {
        startPanel.SetActive(false);
    }

    private void ShowDefeatPanel()
    {
        defeatPanel.SetActive(true);
    }

    private void ShowPassedPanel()
    {
        passedPanel.SetActive(true);
    }

    private void OnEnable()
    {
        cart.CollisionStone.AddListener(OnPlayerLose);
    }

    private void OnDisable()
    {
        cart.CollisionStone.RemoveListener(OnPlayerLose);
    }

    private void OnPlayerLose()
    {
        ShowDefeatPanel();
        Time.timeScale = 0f;
        CancelInvoke(nameof(CheckForRemainingStones)); 
    }

    public void CheckForRemainingStones()
    {
        if (!startPanel.activeSelf && FindObjectsOfType<Stone>().Length == 0)
        {
            ShowPassedPanel();
            CancelInvoke(nameof(CheckForRemainingStones));
        }
    }

    public void RegisterNewStone()
    {
        totalStones++;
        //progressPanel.AddToTotalStones(1);
    }

    public void StoneDestroyed()
    {
        destroyedStones++;
        //progressPanel.StoneDestroyed();
    }
}
