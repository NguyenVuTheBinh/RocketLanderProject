using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI yourCoin;
    private int coinAmount;
    

    private void Awake()
    {
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
        GameManager.Instance.OnCoinChange += GameManager_OnCoinChange; ;

        Hide();
    }

    private void GameManager_OnCoinChange(object sender, System.EventArgs e)
    {
        yourCoin.text = "Current coin: " + GameManager.Instance.GetCoin();
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.LandingOnShop)
        {
            Show();
        }
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
