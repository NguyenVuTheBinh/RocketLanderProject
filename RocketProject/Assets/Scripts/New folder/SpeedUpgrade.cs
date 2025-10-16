using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpgrade : MonoBehaviour
{
    public static SpeedUpgrade Instance { get; private set; }

    [SerializeField] private Button upgradeSpeedButton;
    [SerializeField] private TextMeshProUGUI speedLevelText;

    private static int speedLevel = 1;
    private int speedScalePerLevel = 200;
    private void Awake()
    {
        Instance = this;
        upgradeSpeedButton.onClick.AddListener(() =>
        {
            speedLevel++;
            Lander.Instance.UpgradePushForce(speedScalePerLevel);
            speedLevelText.text = "Speed level " + speedLevel;
        });
    }
    private void Start()
    {
        speedLevelText.text = "Speed level " + speedLevel;
    }
    public void ResetSpeedLevel()
    {
        speedLevel = 1;
    }
}
