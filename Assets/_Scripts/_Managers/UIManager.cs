using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Instance Method
    public static UIManager Instance;
    private void InstanceMethod()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    #endregion

    #region Constant
    [HideInInspector]public TextMeshProUGUI levelIndex;
    [HideInInspector]public GameObject startMenu,levelComplete,levelFailed,confetti;
    
    [HideInInspector]public GameObject myTutorial;
    [HideInInspector]public GameObject optionsMenu;
    [HideInInspector]public GameObject shopMenu;
    [HideInInspector]public GameObject backButton;
    #endregion
    
    
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void _GameInStartMenu()
    {
        myTutorial.SetActive(false);
        levelIndex.enabled = true;
        startMenu.SetActive(false);
        GameManager.Instance.gameState = GameManager.GameState.Play;
    }

    public void _GameWin()
    {
        levelIndex.enabled = false;
        levelComplete.SetActive(true);
        confetti.SetActive(true);
    }

    public void _GameLose()
    {
        levelIndex.enabled = false;
        levelFailed.SetActive(true);
    }

    public void _OptionsMenu()
    {
        levelIndex.enabled = false;
        startMenu.SetActive(false);
        myTutorial.SetActive(false);
        shopMenu.SetActive(false);
        optionsMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void _ShopButton()
    {
        startMenu.SetActive(false);
        levelIndex.enabled = false;
        myTutorial.SetActive(false);
        shopMenu.SetActive(true);
        backButton.SetActive(true);
    }

    public void _BackButton()
    {
        levelIndex.enabled = true;
        startMenu.SetActive(true);
        myTutorial.SetActive(true);
        optionsMenu.SetActive(false);
        backButton.SetActive(false);
        shopMenu.SetActive(false);
    }
    
    public void SetLevelIndex()
    {
        levelIndex.text = "Level " + LevelManager.Instance.currentLevelNumber;
    }
}
