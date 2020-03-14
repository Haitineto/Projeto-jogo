using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Equipments
{    
    public Equipment FireSword;
    public Equipment WaterWand;
    public Equipment RockClub;
    public Equipment AirWand;

    public Equipments()
    {
        FireSword = new Equipment() { Name = "Fire sword", AttributePoint = 1, Element = EnumElement.Fire, Enabled = true, Buyed = true, Equiped = true, Price = 100 };
        WaterWand = new Equipment() { Name = "Water wand", AttributePoint = 2, Element = EnumElement.Water, Enabled = true, Buyed = true, Equiped = false, Price = 150 };
        RockClub = new Equipment() { Name = "Rock club", AttributePoint = 3, Element = EnumElement.Earth, Enabled = true, Buyed = false, Equiped = false, Price = 180 };
        AirWand = new Equipment() { Name = "Air wand", AttributePoint = 4, Element = EnumElement.Air, Enabled = false, Buyed = false, Equiped = false, Price = 200 };
    }
}