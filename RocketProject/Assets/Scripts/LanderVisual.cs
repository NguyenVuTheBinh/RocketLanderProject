using UnityEngine;

public class LanderVisual : MonoBehaviour
{
    //Thruster particle system will be named as Engine for short
    [SerializeField] private ParticleSystem leftEngine;
    [SerializeField] private ParticleSystem middleEngine;
    [SerializeField] private ParticleSystem rightEngine;

    [SerializeField] private GameObject explosionLanderVfx;

    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();

        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnBeforeForce += Lander_OnBeforeForce;
        lander.OnLanded += Lander_OnLanded;

        SetEnabledEngineParticleSystem(leftEngine, false);
        SetEnabledEngineParticleSystem(middleEngine, false);
        SetEnabledEngineParticleSystem(rightEngine, false);
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
            return;
        else
        {
            Instantiate(explosionLanderVfx, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        SetEnabledEngineParticleSystem(leftEngine, false);
        SetEnabledEngineParticleSystem(middleEngine, false);
        SetEnabledEngineParticleSystem(rightEngine, false);
    }
    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnabledEngineParticleSystem(leftEngine, true);
        SetEnabledEngineParticleSystem(middleEngine, true);
        SetEnabledEngineParticleSystem(rightEngine, true);
    }
    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        SetEnabledEngineParticleSystem(leftEngine, true);
    }
    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnabledEngineParticleSystem(rightEngine, true);
    }

    private void SetEnabledEngineParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}
