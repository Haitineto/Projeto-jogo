using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialController : MonoBehaviour
{    
    [SerializeField]
    public CanvasFade CanvasFade;

    [SerializeField]
    public SetupPopUp SetupPopUp;

    [SerializeField]
    public CreditsPopUp CreditsPopUp;

    private void loadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void StartButtonClick()
    {
        SoundManager.instance.PlayButtonClick();
        SoundManager.instance.musicSource.Stop();
        CanvasFade.StartFadeIn(loadMapScene);
    }

    public void Start()
    {
        CanvasFade.StartFadeOut(null);
        SoundManager.instance.PlayMusic(CustomResources.Load<AudioClip>("Sounds/Music/ThemeSong"));
        PlayerPrefs.DeleteAll();
    }        

    public void SetupButtonClick()
    {
        SetupPopUp.Show();
    }

    public void CreditsButtonClick()
    {
        CreditsPopUp.Show();
    }

}
