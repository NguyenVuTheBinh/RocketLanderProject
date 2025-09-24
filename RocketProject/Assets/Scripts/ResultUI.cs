using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultLanding;
    [SerializeField] private TextMeshProUGUI finalStats;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
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
        } else if (e.landingType == Lander.LandingType.LandingOnTerrain)
        {
            resultLanding.text = "<color=#FF0000>CRASH!</color>";
        } else
        {
            resultLanding.text = "<color=#FFAA00>LANDING FAIL!</color>";
        }

        float finalScore = e.score + GameManager.Instance.GetScore();

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
