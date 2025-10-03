using UnityEngine;

public class CoinVisual : MonoBehaviour
{
    [SerializeField] private GameObject coinSprite;
    void Update()
    {
        float rotationSpeed = 33f;
        coinSprite.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
