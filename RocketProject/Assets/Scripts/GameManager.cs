using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static int levelNumber = 1;
    [SerializeField] private List<GameLevel> gameLevelList;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float score;
    private float coinScore = 500f;
    private float time;
    private bool isTimerActive;

    private void Start()
    {
        Instance = this;

        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;

        LoadCurrentLevel();
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
         foreach (GameLevel gameLevel in gameLevelList)
        {
            if (gameLevel.GetLevelNumber() == levelNumber)
            {
                GameLevel spawnGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
                Lander.Instance.transform.position = spawnGameLevel.GetSpawnPosition();
                cinemachineCamera.Target.TrackingTarget = spawnGameLevel.GetInitialCameraTarget();
                CinemachineCameraZoom2D.Instance.SetTargetCameraOrthographicSize(spawnGameLevel.GetInitialCameraOrthographicSize());
            }
        }
    }
    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickup(object sender, System.EventArgs e)
    {
        AddScore(coinScore);
    }

    private void AddScore(float addScoreAmount)
    {
        score += addScoreAmount;
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
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }
}
