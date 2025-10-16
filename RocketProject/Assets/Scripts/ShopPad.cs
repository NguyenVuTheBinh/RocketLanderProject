using UnityEngine;

public class ShopPad : MonoBehaviour
{
    [SerializeField] private Transform spawnAfterShop;

    private void Start()
    {
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.LandingOnShop)
        {
            Lander.Instance.gameObject.transform.position = spawnAfterShop.position;
            Lander.Instance.gameObject.transform.rotation = spawnAfterShop.rotation;
        }
    }
}
