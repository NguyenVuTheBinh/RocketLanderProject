using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {  get; private set; }
    private const int MAX_MUSIC_VOLUME = 10;
    private static float musicTime;
    private static int musicVolume = 6;
    public event EventHandler OnMusicChange;
    
    private AudioSource musicAudioSource;

    private void Awake()
    {
        Instance = this;

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
    }
    private void Start()
    {
        GetMusicVolumeNormalized();
    }

    private void Update()
    {
        musicTime = musicAudioSource.time;
    }
    public void ChangeMusicVolume()
    {
        musicVolume = musicVolume + 1;
        if (musicVolume > MAX_MUSIC_VOLUME)
        {
            musicVolume = 0;
        }
        musicAudioSource.volume = GetMusicVolumeNormalized();
        OnMusicChange?.Invoke(this, EventArgs.Empty);
    }
    public int GetMusicVolume()
    {
        return musicVolume;
    }
    public float GetMusicVolumeNormalized()
    {
        return ((float)musicVolume) / MAX_MUSIC_VOLUME;
    }
}
