using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance = null;
    public AudioSource musicSource;
    public AudioSource effectSource;
    public AudioSource buttonSource;
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ChangeMusicVolume(Settings.MusicVolume);
        ChangeEffectVolume(Settings.EffectVolume);        
    }

    public void PlayEffect(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();        
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        buttonSource.clip = buttonClickSound;
        buttonSource.Play();
    }

    public void RandomizePlayEffect(params AudioClip[] clips)
    {
        var random = Random.Range(0, clips.Length);
        PlayEffect(clips[random]);
    }
    
    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void ChangeEffectVolume(float volume)
    {
        effectSource.volume = volume;
        buttonSource.volume = volume;
    }

}
