using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : MonoBehaviour
{
    public Equipment Equipment;
    public Image EquipImage;   
    public Image ElementImage;    
    public Text AttributeText;    
    public Text NameText;
    public Button UseBuyButton;
    public Text UseBuyButtonCaption;
    public NotifyEvent AfterButtonClick;
    public EquipmentButtonType ButtonType;

    public void DefineEquipment(Equipment equipment)
    {
        Equipment = equipment;
        AttributeText.text = equipment.AttributePoint.ToString();
        NameText.text = equipment.Name.ToString();
        ElementImage.GetComponent<Image>().sprite = CustomResources.Load<Sprite>("Elements/" + equipment.Element);
        EquipImage.GetComponent<Image>().sprite = CustomResources.Load<Sprite>("Equipments/" + equipment.Name);
        UseBuyButton.interactable = Equipment.Enabled;

        if (!Equipment.Buyed)
        {
            UseBuyButtonCaption.text = "$ "+equipment.Price.ToString();
            ButtonType = EquipmentButtonType.Buy;
        }
        else if (!Equipment.Equiped)
        {
            UseBuyButtonCaption.text = "Equip";
            ButtonType = EquipmentButtonType.Equip;
        }
        else
        {
            UseBuyButtonCaption.text = "Using";
            ButtonType = EquipmentButtonType.None;
        }
    }    

    public void ButtonClick()
    {
        if (ButtonType== EquipmentButtonType.Buy)
        {
            Equipment.Buyed = true;
            ButtonType = EquipmentButtonType.Equip;
            PlayerInfo.Coins -= Equipment.Price;
            PlayerInfo.UpdateEquipment(Equipment);
        }
        else if (ButtonType == EquipmentButtonType.Equip)
        {
            Equipment.Equiped = true;
            ButtonType = EquipmentButtonType.None;
            PlayerInfo.UpdateEquipment(Equipment);
        }
        if (AfterButtonClick != null)
            AfterButtonClick();        
    }
}