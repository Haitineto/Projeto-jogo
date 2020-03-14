using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerInfo
{
    #region constants
    private const string CoinsKey = "coins";
    private const string XpKey = "xp";
    private const string LevelKey = "level";
    private const string CurrentBattleKey = "currentbattle";    
    private const string AttributePointsKey = "attributepoints";
    private const string NewLevelReachedKey = "newlevelreached";
    private const string AvaliableAttributePointsKey = "avaliableattributepointskey";
    private const string EquipmentsKey = "equipments";

    public const int DefaultCoins = 1000;
    public const int DefaultXp = 1;
    public const int DefaultLevel = 1;
    public const int DefaultHealth = 1;
    public const int DefaultAir = 1;
    public const int DefaultFire = 1;
    public const int DefaultEarth = 1;
    public const int DefaultWater = 1;
    public const int DefaultLight = 1;
    public const int DefaultCurrentBattle = 1;
    public const int DefaultAvaliableAttributePoints = 20;
    #endregion

    public static void Reset()
    {
        NewLevelReached = "N";
        Level = DefaultLevel;
        CurrentBattle = DefaultCurrentBattle;        
        AvaliableAttributePoints = DefaultAvaliableAttributePoints;
        Coins = DefaultCoins;
        XP = DefaultXp;
        AttributePoints = null;
    }

    public static string NewLevelReached { get { return PlayerPrefs.GetString(NewLevelReachedKey, "N"); } set { PlayerPrefs.SetString(NewLevelReachedKey, value); } }    

    public static int Level { get { return PlayerPrefs.GetInt(LevelKey, DefaultLevel); } set { PlayerPrefs.SetInt(LevelKey, value); } }

    public static int CurrentBattle { get { return PlayerPrefs.GetInt(CurrentBattleKey, DefaultCurrentBattle); } set { PlayerPrefs.SetInt(CurrentBattleKey, value); } }    

    public static int AvaliableAttributePoints { get { return PlayerPrefs.GetInt(AvaliableAttributePointsKey, 0); } set { PlayerPrefs.SetInt(AvaliableAttributePointsKey, value); } }

    public static int Coins { get { return PlayerPrefs.GetInt(CoinsKey, DefaultCoins); } set { PlayerPrefs.SetInt(CoinsKey, value); } }

    public static int XP { get { return PlayerPrefs.GetInt(XpKey, DefaultXp); } set { PlayerPrefs.SetInt(XpKey, value); } }

    public static Equipments Equipments
    {
        get
        {            
            if (PlayerPrefs.GetString(EquipmentsKey, "").Length == 0)
                return new Equipments();
            else                
                return JsonUtility.FromJson<Equipments>(PlayerPrefs.GetString(EquipmentsKey, ""));
        }
        set
        {
            PlayerPrefs.SetString(EquipmentsKey, JsonUtility.ToJson(value));
        }
    }

    public static Equipment FindEquipmentByName(string equipmentName)
    {
        var equipments = Equipments;

        FieldInfo[] fields = typeof(Equipments).GetFields();

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(Equipment))
            {
                Equipment equip = (Equipment)field.GetValue(equipments);
                if (equip.Name.ToLower() == equipmentName.ToLower())
                {
                    return equip;
                }
            }
        }

        return null;
    }

    public static void UpdateEquipment(Equipment equipment)
    {
        var equipments = Equipments;

        FieldInfo[] fields = typeof(Equipments).GetFields();

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(Equipment))
            {
                Equipment equip = (Equipment)field.GetValue(equipments);
                if (equip.Name.ToLower()==equipment.Name.ToLower())
                {
                    equip.Enabled = equipment.Enabled;
                    equip.Buyed = equipment.Buyed;
                    equip.Equiped = equipment.Equiped;
                }
            }
        }

        Equipments = equipments;

        UnequipOtherEquipments(equipment);
    }

    private static void UnequipOtherEquipments(Equipment equipment)
    {
        if (!equipment.Equiped) return;        

        var equipments = Equipments;

        FieldInfo[] fields = typeof(Equipments).GetFields();

        foreach (var field in fields)
        {
            if (field.FieldType == typeof(Equipment))
            {
                Equipment equip = (Equipment)field.GetValue(equipments);
                if (equip.Name.ToLower() != equipment.Name.ToLower())
                {                    
                    equip.Equiped = false;
                }
            }
        }

        Equipments = equipments;
    }

    public static AttributePoints AttributePoints
    {        
        get
        {
            if (PlayerPrefs.GetString(AttributePointsKey, "").Length == 0)
                return DefaultAttributePoints();
            else
                return JsonUtility.FromJson<AttributePoints>(PlayerPrefs.GetString(AttributePointsKey, ""));
        }
        set { PlayerPrefs.SetString(AttributePointsKey, JsonUtility.ToJson(value)); }
    }

    public static AttributePoints DefaultAttributePoints()
    {
        return new AttributePoints() { Health = DefaultHealth, Air = DefaultAir, Earth = DefaultEarth, Fire = DefaultFire, Light = DefaultLight, Water = DefaultWater };
    }

    public static int CalcHealth()
    {
        return ((AttributePoints.Health * 10) * PlayerInfo.Level) + 100;
    }    
}
