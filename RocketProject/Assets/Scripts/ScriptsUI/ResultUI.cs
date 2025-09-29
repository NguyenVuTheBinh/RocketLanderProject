using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultLanding;
    [SerializeField] private TextMeshProUGUI finalStats;
    [SerializeField] private TextMeshProUGUI retryAndContinueButtonText;
    [SerializeField] private Button retryAndContinueButton;

    private Action continueTheGame;
    private void Awake()
    {
        retryAndContinueButton.onClick.AddListener(() =>
        {
            continueTheGame();
        });
    }
    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;

        Hide();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            resultLanding.text = "<color=#00FF00>SUCCESSFUL LANDING!</color>";
            retryAndContinueButtonText.text = "CONTINUE";
            continueTheGame = GameManager.Instance.GetNextLevel;
        } else if (e.landingType == Lander.LandingType.LandingOnTerrain)
        {
            resultLanding.text = "<color=#FF0000>CRASH!</color>";
            retryAndContinueButtonText.text = "RETRY";
            continueTheGame = GameManager.Instance.RetryLevel;
        }
        else
        {
            resultLanding.text = "<color=#FFAA00>LANDING FAIL!</color>";
            retryAndContinueButtonText.text = "RETRY";
            continueTheGame = GameManager.Instance.RetryLevel;
        }

        float finalScore = GameManager.Instance.GetScore();

        finalStats.text = Mathf.Round(e.relativeVelocity)*2 + "\n" +
            Mathf.Round(90 - (Mathf.Acos(e.angleLanding) * 180 / Mathf.PI)) + "\n" +
            "x" + e.multiplier + "\n" +
            GameManager.Instance.GetTime() + "\n" +
            finalScore;
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
