using UnityEngine;
using UnityEngine.UI;

public class SetupPopUp : MessagePopUp
{
    private Canvas _canvas;
    private float _musicVolume;
    private float _effectVolume;
    public Slider MusicSlider;
    public Slider EffectSlider;
    public Animator SetupCanvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        MusicSlider.value = _musicVolume = SoundManager.instance.musicSource.volume;
        EffectSlider.value = _effectVolume = SoundManager.instance.effectSource.volume;
    }


    public void ChangeMusicVolume(float value)
    {
        Settings.MusicVolume = value;
        SoundManager.instance.ChangeMusicVolume(value);
    }

    public void ChangeEffectVolume(float value)
    {
        Settings.EffectVolume = value;
        SoundManager.instance.ChangeEffectVolume(value);
    }

    public override void Show()
    {
        SetupCanvas.SetBool("Enabled", true);
        base.Show();
    }

    public override void Hide()
    {
        SetupCanvas.SetBool("Enabled", false);
        base.Hide();
    }
}
