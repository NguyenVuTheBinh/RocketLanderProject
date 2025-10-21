using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpgrade : MonoBehaviour
{
    public static SpeedUpgrade Instance { get; private set; }

    [SerializeField] private Button upgradeSpeedButton;
    [SerializeField] private TextMeshProUGUI speedLevelText;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private GameObject maxLevelAnnouncement;

    private static int speedLevel = 1;
    private int maxLevel = 5;
    private int speedScalePerLevel = 200;
    private int upgradeCost;
    private int costPerLevel = 100;

    private int coinAmount;
   
    private void Awake()
    {
        Instance = this;
        
        upgradeSpeedButton.onClick.AddListener(() =>
        {
            UpgradeSpeed();
        });
    }
    private void Start()
    {
        GameManager.Instance.OnCoinChange += GameManager_OnCoinChange;
        speedLevelText.text = "Speed level " + speedLevel;
        GetUpgradeCost();
        SetCostText();
        CheckMaxLevel();
    }
    private void SetCostText()
    {
        cost.text = "Cost: " + upgradeCost;
        if (upgradeCost > GameManager.Instance.GetCoin())
        {
            cost.text = "Cost: " + "<color=#FF0000>" + upgradeCost + "</color>";
        }
    }

    private void GameManager_OnCoinChange(object sender, System.EventArgs e)
    {
        coinAmount = GameManager.Instance.GetCoin();
        SetCostText();
    }

    public void ResetSpeedLevel()
    {
        speedLevel = 1;
    }
    public void GetUpgradeCost()
    {
        upgradeCost = speedLevel * costPerLevel;
    }
    private void CheckMaxLevel()
    {
        if (speedLevel == maxLevel)
        {
            maxLevelAnnouncement.SetActive(true);
            upgradeCost = 0;
        }
        else
        {
            maxLevelAnnouncement.SetActive(false);
        }
    }
    private void UpgradeSpeed()
    {
        GetUpgradeCost();
        if (upgradeCost <= GameManager.Instance.GetCoin())
        {
            speedLevel++;
            Lander.Instance.UpgradePushForce(speedScalePerLevel);
            GameManager.Instance.AddCoin(-upgradeCost);
            speedLevelText.text = "Speed level " + speedLevel;
            GetUpgradeCost();
            CheckMaxLevel();
        }
        else
        {
            Debug.Log("DO SOMETHING, THEY ARE SPAMMING");
            return;
        }
        SetCostText();
    }
}
