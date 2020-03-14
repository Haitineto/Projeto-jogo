using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapButton : MonoBehaviour {

    public int BattleId = 0;

    public Text Label;

    public void Start()
    {
        Label.text = BattleId.ToString();
        if (BattleId > PlayerInfo.CurrentBattle)
        {
            Label.color = new Color(Label.color.r, Label.color.g, Label.color.b, 0.5f);
            GetComponent<Button>().enabled = false;
        }            
    }

    public void Click()
    {
        if (BattleId<=PlayerInfo.CurrentBattle)
        {
            GameData.BattleToLoad = BattleId;

            GameObject canvasObject = GameObject.Find("BlackCanvas");

            CanvasFade canvasFade = canvasObject.GetComponent<CanvasFade>();

            canvasFade.StartFadeIn(LoadBattleScene);           
        }
    }

    private void LoadBattleScene()
    {
        SceneManager.LoadScene("BossCardScene");
    }
}
