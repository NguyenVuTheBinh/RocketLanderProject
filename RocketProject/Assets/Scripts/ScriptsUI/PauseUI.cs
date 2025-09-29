using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button returnMenuButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnpauseGame();
        });
        returnMenuButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadScene(SceneLoader.Scene.MenuScene);
        });

        soundButton.onClick.AddListener(() => 
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundText.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            musicText.text = "MUSIC " + MusicManager.Instance.GetMusicVolume();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnpause += GameManager_OnGameUnpause;

        Hide();

        soundText.text = "SOUND " + SoundManager.Instance.GetSoundVolume();
        musicText.text = "MUSIC " + MusicManager.Instance.GetMusicVolume();
    }

    private void GameManager_OnGameUnpause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePause(object sender, System.EventArgs e)
    {
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
