using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Button returnMenuButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    private void Awake()
    {
        returnMenuButton.onClick.AddListener(() =>{
            SceneLoader.LoadScene(SceneLoader.Scene.MenuScene);
        });
        quitGameButton.onClick.AddListener(() =>{
            Application.Quit();
        });
    }
    private void Start()
    {
        Debug.Log(GameManager.Instance.GetTotalScore());
        totalScoreText.text = "Total score: " + Mathf.Round(GameManager.Instance.GetTotalScore()).ToString();
    }
}
