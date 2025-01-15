using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : GenericSingleton<UiManager> 
{
    [SerializeField] private Button[] levelButtons;
    EventManager eventManager;
    GameManager gameManager;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    public Action rightButtonOnClick;
    public Action leftButtonOnClick;

    [SerializeField] TextMeshPro ballCountText;
    [SerializeField] TextMeshPro targetScoreText;

    public GameObject mainPanel;
    [SerializeField] public GameObject finishPanel;
    [SerializeField] GameObject roadPanel;
    public GameObject[] selectLevelPanels;
    [SerializeField] GameObject areaPanel;
    [SerializeField] GameObject settingsPanel;

    private bool buttonActive;
    [SerializeField] Button pauseButton;
    //[SerializeField] Image restartImage;
    [SerializeField] Image mainMenuImage;
    [SerializeField] Image restartImage;

    [SerializeField] TextMeshProUGUI diamondCount;

    [SerializeField] TextMeshProUGUI currentCoinText;
    [SerializeField] TextMeshProUGUI currentDiamondText;
    [SerializeField] TextMeshProUGUI levelText;

    [SerializeField] List<Button> worlds;
    [SerializeField] List<GameObject> worldLocks;

    [SerializeField] Image roadImage;

    private bool _buttonActive = true;
    int selectLevelIndex;

    int targetScore;

    SaveSystem saveSystem = SaveSystem.Instance;

    private Dictionary<string,IButtonAction> buttonActions = new Dictionary<string,IButtonAction>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);

        SetCollection();
        gameManager = GameManager.Instance;
        eventManager = EventManager.Instance;

        eventManager.onChangedLevel += UpdateTextReferances;
        eventManager.onChangedLevel += UpdateTexts;
        eventManager.onChangedLevel += CloseFinishPanel;
        eventManager.onScored += UpdateTargetScoreText;
        eventManager.onClick += UpdateTexts;
        eventManager.onFinish += GetFinishPanel;
        eventManager.onStart += SetUiButtons;
        eventManager.onFinish += SetUiButtons;

        ChechkWorlds();
        CheckLevelButtons();
      /*  if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
            mainPanel.gameObject.SetActive(true);
        }*/
    }
    private void CheckLevelButtons()
    {
        int i = 0;
        while(i <= SaveSystem.data.lastLevel)
        {
            levelButtons[i].interactable = true;
            i++;
        }
    }
    
    private void ChechkWorlds()
    {
        int worldIndex = (SaveSystem.data.lastLevel - 1) / 15 + 1;

        for(int i = 0; i < worldIndex;i++)
        {
            worlds[i].gameObject.SetActive(true);
            worldLocks[i].gameObject.SetActive(false);
        }
    }

    public void SetSelectLevelPanel(int index)
    {
        selectLevelPanels[index].gameObject.SetActive(true);
        selectLevelIndex = index;
    }
    public void RightButton()
    {
        rightButtonOnClick?.Invoke();
    }
    public void LeftButton()
    {
        leftButtonOnClick?.Invoke();
    }
 
    public void UpdateTexts()
    {
        ballCountText.text = $"{gameManager.ballCount}";
        targetScoreText.text = $"{gameManager.targetScore}";
    }
    public void UpdateTargetScoreText()
    {
        if(gameManager.targetScore > 0)
            targetScoreText.text = $"{--gameManager.targetScore}";
    }

    public void OnButtonClick(string buttonName)
    {
        buttonActions[buttonName].Execute();
    }

   /* public void PauseButton()
    {
       buttonActive = !buttonActive;

        if (buttonActive)
        {
            Time.timeScale = 0;
            restartImage.gameObject.SetActive(true);
            mainMenuImage.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            restartImage.gameObject.SetActive(false);
            mainMenuImage.gameObject.SetActive(false);
        }
    }*/

    public void RewardText()
    {
        currentCoinText.text = $"{gameManager.coin}";
        currentDiamondText.text = $"{gameManager.diamond}";
    }
    public void GetFinishPanel()
    {
        StartCoroutine(WaitFinishPanel());
    }

    private IEnumerator WaitFinishPanel()
    {
        RewardText();
        yield return new WaitForSeconds(0.6f);
        finishPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UpdateTextReferances()
    {
        ballCountText = GameObject.FindWithTag("BallCount").GetComponent<TextMeshPro>();
        targetScoreText = GameObject.FindWithTag("TargetScore").GetComponent<TextMeshPro>();
    }
    public void GetRoadPanel()
    {
        roadPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void SetCollection()
    {
        buttonActions.Add("Play", new PlayButtonHandler());
        buttonActions.Add("SelectLevel", new SelectLevelButtonHandler());
        buttonActions.Add("NextLevel",new NextLevelButtonHandler());
        buttonActions.Add("Restart",new RestartButtonHandler());
        buttonActions.Add("BackMenu",new BackToMainMenu());
        buttonActions.Add("Exit", new ExitButtonHandler());
    }
    private void CloseFinishPanel()
    {
        finishPanel.SetActive(false);
        selectLevelPanels[selectLevelIndex].SetActive(false);
    }
    public void UpdateLevelText()
    {
        levelText.text = $"Level {LevelManager.Instance.currentLevel}";
    }

    public void SelectAreaButton()
    {
        roadPanel.SetActive(false);
        selectLevelPanels[selectLevelIndex].SetActive(true);
    }
    private void SetUiButtons()
    {
        _buttonActive = !_buttonActive;
        if (_buttonActive)
        {
            pauseButton.gameObject.SetActive(_buttonActive);
            roadImage.gameObject.SetActive(_buttonActive);
            restartImage.gameObject.SetActive(!_buttonActive);
        }
        else
        {
            pauseButton.gameObject.SetActive(_buttonActive);
            roadImage.gameObject.SetActive(_buttonActive);
            restartImage.gameObject.SetActive(!_buttonActive);
        }
    }
    public void BackButton(int i)
    {
        if (i == 0)
        {
            roadPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            roadPanel.gameObject.SetActive(true);
            selectLevelPanels[selectLevelIndex].gameObject.SetActive(false);
        }
    }
    private bool settingsButton;
    public void SettingsButton()
    {
        settingsButton = !settingsButton;

        if (settingsButton)
            settingsPanel.SetActive(true);
        else
            settingsPanel.SetActive(false);
    }

}
