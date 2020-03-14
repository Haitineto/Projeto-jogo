using UnityEngine;

public class Settings
{
    #region constants
    private const string MusicVolumeKey = "musicvolume";
    private const string EffectVolumeKey = "effectvolume";

    public const float DefaultMusicVolume = 0.5f;
    public const float DefaulEffectVolume = 0.5f;
    #endregion

    public static void Reset()
    {
        MusicVolume = DefaultMusicVolume;
        EffectVolume = DefaulEffectVolume;
    }

    public static float MusicVolume { get { return PlayerPrefs.GetFloat(MusicVolumeKey, DefaultMusicVolume); } set { PlayerPrefs.SetFloat(MusicVolumeKey, value); } }
    public static float EffectVolume { get { return PlayerPrefs.GetFloat(EffectVolumeKey, DefaulEffectVolume); } set { PlayerPrefs.SetFloat(EffectVolumeKey, value); } }
}
