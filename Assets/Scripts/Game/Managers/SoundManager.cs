using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource _soundEfectsManager;
    private AudioSource _audioSourceMusic;
    public AudioSource audioSourceMusic{get{return _audioSourceMusic;}}
    [SerializeField] private AudioClip _tapButton;
    private float _volumeSound = 0.25f;
    public static bool soundVolumeStatus;
    public static bool musicVolumeStatus;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
        soundVolumeStatus = true;
        musicVolumeStatus = true;
        _audioSourceMusic = GetComponent<AudioSource>();
    } 

    public void PlaySoundEfects(AudioClip sound)
    {
        if (_soundEfectsManager != null && sound != null)
        {
            _soundEfectsManager.volume = _volumeSound;
            _soundEfectsManager.clip = sound;
            _soundEfectsManager.Play();
        }
    }

    public void TapButton()
    {
        PlaySoundEfects(_tapButton);
    }

    public void SetVolumeSound(bool state)
    {
        soundVolumeStatus = state;
        _soundEfectsManager.mute = !state; 
    }

    public void SetVolumeMusic(bool state)
    {
        musicVolumeStatus = state;
        _audioSourceMusic.mute = !state;
    }
}