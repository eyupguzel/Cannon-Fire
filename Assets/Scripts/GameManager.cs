using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.EventSystems;

public class GameManager : GenericSingleton<GameManager>
{
    public GameObject[] balls;

    public static int score;
    [HideInInspector] public int targetScore;
    [HideInInspector] public int ballCount;
    [HideInInspector] public int maxTargetScore;
    private int ballsInAir;


    SaveSystem saveSystem = SaveSystem.Instance;

    public int coin;
    public int diamond;

    [SerializeField] GameObject diamondObject;
    [SerializeField] GameObject diamondVfx;

    [SerializeField] Color[] color;
    [SerializeField] List<ParticleSystem> particleEffects;

    private bool startClick;
    void Awake()
    {
        Application.targetFrameRate = 60;
        saveSystem.LoadData();

        DontDestroyOnLoad(gameObject);

        EventManager.Instance.onChangedLevel += SetTimeScale;
        EventManager.Instance.onFinish += DeactivateBalls;
        EventManager.Instance.onFinish += SaveData;
        EventManager.Instance.onCollectable += GetDiamondVfx;
        EventManager.Instance.onBackMenu += DeactivateBalls;
        EventManager.Instance.onBackMenu += LoadData;
    }

    float timer;
    float delay = 0.3f;
    private void SaveData()
    {
        saveSystem.SaveData();
    }
    private void LoadData()
    {
        saveSystem.LoadData();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                ButtonControl();
                timer = 0;
            }
        }
    }

    private void ButtonControl()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ballCount--;
            if (ballCount >= 0)
                EventManager.Instance.OnClick();
            if (!startClick)
            {
                EventManager.Instance.OnStart();
                startClick = true;
            }
        }
    }
    public void CheckFinish()
    {
        /*if (targetScore == 0)
        {
            FinishGame("Win");
        }
        else if (ballCount == 0 || ballCount < targetScore - 1)
        {
            FinishGame("Lose");
        }*/
        if (ballCount <= 0)
        {
            if (targetScore <= 0)
                FinishGame("Win");
            else
                FinishGame("Lose");
        }
    }

    private void FinishGame(string result)
    {
        if (result == "Win")
        {
            LevelState _state = SaveSystem.data.levelStates.Find(state => state.level == LevelManager.Instance.currentLevel);
            if (_state == null)
            {
                AudioManager.Instance.WinSound();
                SaveSystem.data.levelStates.Add(new LevelState(LevelManager.Instance.currentLevel, true, 0));
                //SaveSystem.data.totalGold += ballCount * 7;
                SaveSystem.data.totalDiamond += diamond;
            }
        }
        else
        {
            SaveSystem.data.levelStates.Add(new LevelState(LevelManager.Instance.currentLevel, false, 0));
        }

        EventManager.Instance.OnFinish();
    }

    public void SetTargetScore(int targetScore) => this.targetScore = targetScore;
    public void SetBallCount(int ballCount) => this.ballCount = ballCount;
    public void GetDiamond(Vector3 position)
    {
        diamondObject.transform.position = position;
        diamondObject.SetActive(true);
    }
    //public void GetDiamond() => diamondObject.SetActive(true);
    private void SetTimeScale()
    {
        coin = 0;
        startClick = false;

        Time.timeScale = 1;
        ChangeBallColor();
    }

    public void DeactivateBalls()
    {
        foreach (GameObject item in balls)
        {
            item.SetActive(false);
        }
    }

    private void ChangeBallColor()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            int randomColor = UnityEngine.Random.Range(0, color.Length);
            balls[i].gameObject.GetComponent<Renderer>().material.color = color[randomColor];

        }
    }

    private void GetDiamondVfx(object position)
    {
        diamondVfx.transform.position = (Vector3)position;
        diamondVfx.gameObject.SetActive(true);
    }
    public void GetParticalEffect(Vector3 position, Color color)
    {
        foreach (ParticleSystem item in particleEffects)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.transform.position = position;
                item.GetComponent<ParticleSystem>().startColor = color;
                item.gameObject.SetActive(true);
                break;
            }
        }
    }

    /* public void CalculateCoins()
     {
         LevelState currentLevel = SaveSystem.data.levelStates.Find(level => level.level == LevelManager.Instance.currentLevel);

         if (!currentLevel.isComplete)
         {

             Debug.Log("Altýn toplandý");
         }
         else
             Debug.Log("Altýn toplanmadý");
     }*/
}
