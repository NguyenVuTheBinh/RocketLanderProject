using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStatsUI;

    [SerializeField] private Image velocityUpArrow;
    [SerializeField] private Image velocityDownArrow;
    [SerializeField] private Image velocityRightArrow;
    [SerializeField] private Image velocityLeftArrow;

    [SerializeField] private Image imageFuelAmount;

    private void FixedUpdate()
    {
        UpdateStatsTextMesh();
    }

    private void UpdateStatsTextMesh()
    {
        float speedX = Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedX())) * 10;
        float speedY = Mathf.Abs(Mathf.Round(Lander.Instance.GetSpeedY())) * 10;
        velocityUpArrow.gameObject.SetActive(speedY >= 0);
        velocityDownArrow.gameObject.SetActive(speedY < 0);
        velocityRightArrow.gameObject.SetActive(speedY >= 0);
        velocityLeftArrow.gameObject.SetActive(speedY < 0);

        textStatsUI.text = GameManager.Instance.GetCurrentLevel() + "\n" +
            GameManager.Instance.GetScore() + "\n" +
            GameManager.Instance.GetTime() + "\n" +
            speedX + "\n" +
            speedY + "\n";

        imageFuelAmount.fillAmount = Lander.Instance.GetFuelAmount();
    }
}
