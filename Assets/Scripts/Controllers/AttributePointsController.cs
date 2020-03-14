using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttributePointsController : MonoBehaviour
{
    private const int _necessaryCoinsToRestart = 1000;

    private AttributePoints _originalAttributePoints;

    private AttributePoints _currentAttributePoints;

    private int _originalCoins;

    private int _currentCoins;

    private int _originalAvaliableAttributePoints;

    private int _currentAvaliableAttributePoints;        

    public Text AvaliableAttributePoints;

    public Text AvaliableCoins;

    [SerializeField]
    public AttributePointsPanel FirePanel;

    [SerializeField]
    public AttributePointsPanel WaterPanel;

    [SerializeField]
    public AttributePointsPanel EarthPanel;

    [SerializeField]
    public AttributePointsPanel AirPanel;

    [SerializeField]
    public AttributePointsPanel LightPanel;

    [SerializeField]
    public AttributePointsPanel HealthPanel;

    [SerializeField]
    public CanvasFade CanvasFade;

    public Button ConfirmaButton;

    void Start()
    {
        //PlayerInfo.Reset();
        CanvasFade.StartFadeOut(null);
        LoadData();
        SetupComponents();
    }

    public void SetupComponents()
    {
        ConfirmaButton.interactable = _currentCoins >= _necessaryCoinsToRestart;

        AvaliableCoins.text = _currentCoins.ToString();
        AvaliableAttributePoints.text = _currentAvaliableAttributePoints.ToString();        

        FirePanel.OnIncAttributePoints = onIncAttributePoints;
        FirePanel.OnDecAttributePoints = onDecAttributePoints;
        FirePanel.UpdateText(_currentAttributePoints.Fire);

        WaterPanel.OnIncAttributePoints = onIncAttributePoints;
        WaterPanel.OnDecAttributePoints = onDecAttributePoints;
        WaterPanel.UpdateText(_currentAttributePoints.Water);

        EarthPanel.OnIncAttributePoints = onIncAttributePoints;
        EarthPanel.OnDecAttributePoints = onDecAttributePoints;
        EarthPanel.UpdateText(_currentAttributePoints.Earth);

        AirPanel.OnIncAttributePoints = onIncAttributePoints;
        AirPanel.OnDecAttributePoints = onDecAttributePoints;
        AirPanel.UpdateText(_currentAttributePoints.Air);

        LightPanel.OnIncAttributePoints = onIncAttributePoints;
        LightPanel.OnDecAttributePoints = onDecAttributePoints;
        LightPanel.UpdateText(_currentAttributePoints.Light);

        HealthPanel.OnIncAttributePoints = onIncAttributePoints;
        HealthPanel.OnDecAttributePoints = onDecAttributePoints;
        HealthPanel.UpdateText(_currentAttributePoints.Health);
    }

    public void LoadData()
    {                
        _originalAttributePoints = PlayerInfo.AttributePoints;
        _originalAvaliableAttributePoints = PlayerInfo.AvaliableAttributePoints;
        _originalCoins = PlayerInfo.Coins;
        
        _currentAttributePoints = PlayerInfo.AttributePoints;
        _currentAvaliableAttributePoints = PlayerInfo.AvaliableAttributePoints;
        _currentCoins = PlayerInfo.Coins;
    }    

    public void RestartClick()
    {
        if (_currentCoins < _necessaryCoinsToRestart) return;

        _originalAvaliableAttributePoints = _currentAvaliableAttributePoints +
                                            (_currentAttributePoints.Health - PlayerInfo.DefaultHealth) +
                                            (_currentAttributePoints.Air - PlayerInfo.DefaultAir) +
                                            (_currentAttributePoints.Earth - PlayerInfo.DefaultEarth) +
                                            (_currentAttributePoints.Fire - PlayerInfo.DefaultFire) +
                                            (_currentAttributePoints.Water - PlayerInfo.DefaultWater) +
                                            (_currentAttributePoints.Light - PlayerInfo.DefaultLight);
        _currentAvaliableAttributePoints = _originalAvaliableAttributePoints;
        
        _originalAttributePoints = PlayerInfo.DefaultAttributePoints();
        _originalAvaliableAttributePoints = PlayerInfo.DefaultAvaliableAttributePoints;
        _originalCoins -= _necessaryCoinsToRestart;
        
        _currentAttributePoints = PlayerInfo.DefaultAttributePoints();
        _currentAvaliableAttributePoints = PlayerInfo.DefaultAvaliableAttributePoints;
        _currentCoins -= _necessaryCoinsToRestart;

        SetupComponents();
    }

    public void ConfirmClick()
    {
        PlayerInfo.AttributePoints = _currentAttributePoints;
        PlayerInfo.AvaliableAttributePoints = _currentAvaliableAttributePoints;        
        PlayerInfo.Coins = _currentCoins;
        CanvasFade.StartFadeIn(LoadMapScene);
    }

    private void LoadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    private void onIncAttributePoints(AttributeType attributeType)
    {
        changeAttributePoints(attributeType, +1);
    }    

    private void onDecAttributePoints(AttributeType attributeType)
    {
        changeAttributePoints(attributeType, -1);
    }

    private void changeAttributePoints(AttributeType attributeType, int factor)
    {
        if ((_currentAvaliableAttributePoints - factor) > _originalAvaliableAttributePoints) return;

        if ((_currentAvaliableAttributePoints - factor) < 0) return;

        switch (attributeType)
        {
            case AttributeType.Air:                
                if ((_currentAttributePoints.Air + factor) < _originalAttributePoints.Air) return;
                _currentAttributePoints.Air += factor;
                AirPanel.UpdateText(_currentAttributePoints.Air);
                break;
            case AttributeType.Earth:
                if ((_currentAttributePoints.Earth + factor) < _originalAttributePoints.Earth) return;
                _currentAttributePoints.Earth += factor;
                EarthPanel.UpdateText(_currentAttributePoints.Earth);
                break;
            case AttributeType.Fire:
                if ((_currentAttributePoints.Fire + factor) < _originalAttributePoints.Fire) return;
                _currentAttributePoints.Fire += factor;
                FirePanel.UpdateText(_currentAttributePoints.Fire);
                break;
            case AttributeType.Water:
                if ((_currentAttributePoints.Water + factor) < _originalAttributePoints.Water) return;
                _currentAttributePoints.Water += factor;
                WaterPanel.UpdateText(_currentAttributePoints.Water);
                break;
            case AttributeType.Light:
                if ((_currentAttributePoints.Light + factor) < _originalAttributePoints.Light) return;
                _currentAttributePoints.Light += factor;
                LightPanel.UpdateText(_currentAttributePoints.Light);
                break;
            case AttributeType.Health:
                if ((_currentAttributePoints.Health + factor) < _originalAttributePoints.Health) return;
                _currentAttributePoints.Health += factor;
                HealthPanel.UpdateText(_currentAttributePoints.Health);
                break;
            default:
                break;
        }

        _currentAvaliableAttributePoints -= factor;

        AvaliableAttributePoints.text = _currentAvaliableAttributePoints.ToString();
    }
}
