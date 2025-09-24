using TMPro;
using UnityEngine;

public class LandingPadVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro multiplierVisualText;

    private void Awake()
    {
        LandingPad landingPad = GetComponent<LandingPad>();
        multiplierVisualText.text = "x" + landingPad.GetMultiplierScore();
    }
}
