using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossCardController : MonoBehaviour
{
    private BattleInfo _battleInfo;

    [SerializeField]
    public CanvasFade CanvasFade;

    [SerializeField]
    public Text BossNameText;

    [SerializeField]
    public Text CoinsToEarnText;

    [SerializeField]
    public Text XPToEarnText;

    [SerializeField]
    public Image SurpriseImage;

    [SerializeField]
    public Text AirText;

    [SerializeField]
    public Text FireText;

    [SerializeField]
    public Text HealthText;

    [SerializeField]
    public Text LightText;

    [SerializeField]
    public Text WaterText;

    [SerializeField]
    public Text EarthText;

    public void Start()
    {
        _battleInfo = BattleInfoFactory.CreateBattleInfo(GameData.BattleToLoad);

        initializeComponents();

        CanvasFade.StartFadeOut(null);
    }

    private void initializeComponents()
    {
        SurpriseImage.enabled = _battleInfo.Reward.HasSurprise;
        CoinsToEarnText.text = _battleInfo.Reward.CoinsToEarn.ToString();
        XPToEarnText.text = _battleInfo.Reward.XPToEarn.ToString();

        BossNameText.text = _battleInfo.BossInfo.Name;
        AirText.text = _battleInfo.BossInfo.AttributePoints.Air.ToString();
        FireText.text = _battleInfo.BossInfo.AttributePoints.Fire.ToString();
        LightText.text = _battleInfo.BossInfo.AttributePoints.Light.ToString();
        WaterText.text = _battleInfo.BossInfo.AttributePoints.Water.ToString();
        EarthText.text = _battleInfo.BossInfo.AttributePoints.Earth.ToString();
        HealthText.text = _battleInfo.BossInfo.Health.ToString();
    }

    public void BackClick()
    {
        CanvasFade.StartFadeIn(loadMapScene);
    }

    public void ContinueClick()
    {
        CanvasFade.StartFadeIn(loadBattleScene);
    }

    private void loadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    private void loadBattleScene()
    {
        SceneManager.LoadScene("EquipScene");
    }
}
