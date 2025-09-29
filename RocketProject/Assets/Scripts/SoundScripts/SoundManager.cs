using System;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const int MAX_SOUND_VOLUME = 10;

    private static int soundVolume = 6;

    public static SoundManager Instance { get; private set; }

    public event EventHandler OnSoundChanged;

    [SerializeField] private AudioClip fuelPickUpSoundClip;
    [SerializeField] private AudioClip coinPickUpSoundClip;
    [SerializeField] private AudioClip landedSoundClip;
    [SerializeField] private AudioClip crashedSoundClip;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case Lander.LandingType.Success:
                AudioSource.PlayClipAtPoint(landedSoundClip, Camera.main.transform.position);
                break;

            default:
                AudioSource.PlayClipAtPoint(crashedSoundClip, Camera.main.transform.position);
                break;
        }
    }        

    private void Lander_OnCoinPickup(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickUpSoundClip, Camera.main.transform.position, 
            GetSoundVolumeNormalized());
    }

    private void Lander_OnFuelPickup(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(fuelPickUpSoundClip, Camera.main.transform.position, 
            GetSoundVolumeNormalized());
    }

    public void ChangeSoundVolume()
    {
        soundVolume = soundVolume + 1;
        if (soundVolume > MAX_SOUND_VOLUME)
        {
            soundVolume = 0;
        }
        OnSoundChanged?.Invoke(this, EventArgs.Empty);
    }
    public int GetSoundVolume()
    {
        return soundVolume;
    }
    public float GetSoundVolumeNormalized()
    {
        return ((float)soundVolume) / MAX_SOUND_VOLUME;
    }
}
