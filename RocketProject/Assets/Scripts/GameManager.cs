using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        isTimerActive = e.state == Lander.State.Normal;
    }

    private void Update()
    {
        if(isTimerActive)
            time += Time.deltaTime;
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
}
