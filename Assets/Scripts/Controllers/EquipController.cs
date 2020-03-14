using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipController : MonoBehaviour
{
    [SerializeField]
    public GameObject Parent;

    [SerializeField]
    public GameObject EquipPanelPrefab;

    [SerializeField]
    public Text AirText;

    [SerializeField]
    public Text FireText;

    [SerializeField]
    public Text HearthText;

    [SerializeField]
    public Text LightText;

    [SerializeField]
    public Text WaterText;

    [SerializeField]
    public Text EarthText;

    [SerializeField]
    public Text CoinText;

    private Equipments Equipments;

    private List<EquipPanel> Panels = new List<EquipPanel>();

    // Use this for initialization
    void Start ()
    {
        PlayerInfo.Coins = 500;

        PlayerInfo.Equipments = new Equipments();

        Equipments = PlayerInfo.Equipments;

        var index = 0;

        FieldInfo[] fields = typeof(Equipments).GetFields();

        foreach (var field in fields)
        {            
            if (field.FieldType == typeof(Equipment))
            {
                Equipment equip = (Equipment)field.GetValue(Equipments);

                GameObject equipPanelPrefab = Instantiate(EquipPanelPrefab);
                equipPanelPrefab.transform.SetParent(Parent.transform);
                equipPanelPrefab.transform.localPosition = new Vector3(0, (index + 1) * -200, 0);

                EquipPanel equipPanel = equipPanelPrefab.GetComponent<EquipPanel>();
                equipPanel.DefineEquipment(equip);
                equipPanel.AfterButtonClick = AfterPanelButtonClick;
                Panels.Add(equipPanel);
            }
            index++;
        }            

        var attr = PlayerInfo.AttributePoints;
        AirText.text = attr.Air.ToString();
        FireText.text = attr.Fire.ToString();        
        LightText.text = attr.Light.ToString();
        WaterText.text = attr.Water.ToString();
        EarthText.text= attr.Earth.ToString();
        HearthText.text = attr.Health.ToString();
        CoinText.text = PlayerInfo.Coins.ToString();
    }

    private void AfterPanelButtonClick()
    {
        foreach (var item in Panels)
        {
            item.DefineEquipment(PlayerInfo.FindEquipmentByName(item.Equipment.Name));
            CoinText.text = PlayerInfo.Coins.ToString();
        }
    }

    public void BackButtonClick()
    {
        SceneManager.LoadScene("BossCardScene");
    }

    public void FightButtonClick()
    {
        SceneManager.LoadScene("BattleScene");
    }
}