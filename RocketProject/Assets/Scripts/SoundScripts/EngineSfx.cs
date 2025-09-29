using UnityEngine;

public class EngineSfx : MonoBehaviour
{
    [SerializeField] private AudioSource engineAudioSource;

    private Lander lander;

    private void Awake()
    {
        lander = GetComponent<Lander>();
    }
    private void Start()
    {
        lander.OnBeforeForce += Lander_OnBeforeForce;
        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;

        SoundManager.Instance.OnSoundChanged += SoundManager_OnSoundChanged;

        engineAudioSource.Pause();
    }

    private void SoundManager_OnSoundChanged(object sender, System.EventArgs e)
    {
        engineAudioSource.volume = SoundManager.Instance.GetSoundVolumeNormalized();
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        engineAudioSource.Pause();
    }
}
