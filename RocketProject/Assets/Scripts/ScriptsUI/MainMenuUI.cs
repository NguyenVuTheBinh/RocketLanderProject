using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        Time.timeScale = 1f;
        startButton.onClick.AddListener(() =>{
            GameManager.ResetData();
            SceneLoader.LoadScene(SceneLoader.Scene.InGameScene);
        });
        quitButton.onClick.AddListener(() => { 
            Application.Quit();
        });
    }
}
