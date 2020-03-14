using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    [SerializeField]
    public CanvasFade CanvasFade;

    private void loadInitialScene()
    {
        SceneManager.LoadScene("InitialScene");
    }

    private void loadAttributeScene()
    {
        SceneManager.LoadScene("AttributesScene");
    }

    public void Start ()
    {
        CanvasFade.StartFadeOut(null);
    }    

    public void BackToInitial()
    {
        CanvasFade.StartFadeIn(loadInitialScene);
    }

    public void GoToAttributeManager()
    {
        CanvasFade.StartFadeIn(loadAttributeScene);
    }
}
