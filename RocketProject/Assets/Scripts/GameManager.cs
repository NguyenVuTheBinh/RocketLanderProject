using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    

    private static int levelNumber = 1;
    private static float totalScore = 0;
    private static float time = 0;
    private static int coinAmount = 0;

    public static void ResetData()
    {
        levelNumber = 1;
        totalScore = 0;
        time = 0;
        coinAmount = 0;
        SpeedUpgrade.Instance.ResetSpeedLevel();
    }

    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpause;

    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float score;
    private int coin = 500;
    private bool isTimerActive;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;

        GameInput.Instance.OnPauseButtonPressed += GameInput_OnPauseButtonPressed;
        LoadCurrentLevel();
    }

    private void GameInput_OnPauseButtonPressed(object sender, System.EventArgs e)
    {
        PauseAndUnpauseGame();
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        isTimerActive = e.state == Lander.State.Normal;

        if (e.state == Lander.State.Normal)
        {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.SetNormalOrthographicSize();
        }
    }

    private void Update()
    {
        if(isTimerActive)
            time += Time.deltaTime;

    }
    private void LoadCurrentLevel()
    {
        GameLevel gameLevel = GetGameLevel();
        GameLevel spawnGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = spawnGameLevel.GetSpawnPosition();
        cinemachineCamera.Target.TrackingTarget = spawnGameLevel.GetInitialCameraTarget();
        CinemachineCameraZoom2D.Instance.SetTargetCameraOrthographicSize(spawnGameLevel.GetInitialCameraOrthographicSize());
    }
    private GameLevel GetGameLevel()
    {
        foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                return gameLevel;
            }
        }
        return null;
    }
    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickup(object sender, System.EventArgs e)
    {
        AddCoin(coin);
    }

    private void AddScore(float addScoreAmount)
    {
        score += addScoreAmount;
    }

    private void AddCoin(int addCoinAmount)
    {
        coinAmount += addCoinAmount;
    }

    public float GetTime()
    {
        return Mathf.Round(time);
    }

    public float GetScore()
    {
        return Mathf.Round(score);
    }

    public int GetCurrentLevel()
    {
        return levelNumber;
    }

    public void GetNextLevel()
    {
        levelNumber++;
        totalScore += score;
        if (GetGameLevel() == null)
        {
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
        {
            SceneLoader.LoadScene(SceneLoader.Scene.InGameScene);
        }
    }

    public void RetryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.InGameScene);
    }
    public void PauseAndUnpauseGame()
    {
        if (Time.timeScale == 1f)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePause?.Invoke(this, EventArgs.Empty);
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        OnGameUnpause?.Invoke(this, EventArgs.Empty);
    }
    public float GetTotalScore()
    {
        return totalScore;
    }
}

