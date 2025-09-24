using Unity.VisualScripting;
using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField] private int multiplierScore;

    public int GetMultiplierScore()
    {
        return multiplierScore;
    }
}
