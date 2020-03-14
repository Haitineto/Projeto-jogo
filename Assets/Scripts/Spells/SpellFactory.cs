using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SpellFactory : MonoBehaviour
{
    #region Factory
    public static ISpell Create(EnumElement element, EnumElement? elementLastRuna, List<Runa> selectedRunas, Character origin, Character target, AttributePoints attributes)
    {
        EnumElement element1 = element;
        EnumElement? element2 = elementLastRuna;

        var prefabSpell = CreateSpellPrefab();

        var force = CalculateSpellForce(selectedRunas, attributes);

        switch (element1)
        {
            case EnumElement.Air:
                switch (element2)
                {
                    case EnumElement.Air:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/AirSpell"), force, origin, target, true);
                    case EnumElement.Earth:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/AirSpell"), force, origin, target, false);
                    case EnumElement.Fire:
                        return CreateBurnSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/BurnSpell"), force, attributes, origin, target);
                    case EnumElement.Water:
                        return CreateFreezeTimeSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/FreezeSpell"), attributes);
                    case EnumElement.Light:
                        return CreateTimeSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/AddTimeSpell"), attributes);
                    case null:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/AirSpell"), force, origin, target, false);
                    default:
                        return null;
                }
            case EnumElement.Earth:
                switch (element2)
                {
                    case EnumElement.Air:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/EarthSpell"), force, origin, target, false);
                    case EnumElement.Earth:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/EarthSpell"), force, origin, target, true);
                    case EnumElement.Fire:
                        return CreateLavaSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/LavaSpell"), force, attributes, origin, target);
                    case EnumElement.Water:
                        return CreateRootingSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/RootingSpell"), force, attributes, origin, target);
                    case EnumElement.Light:
                        return CreateProtectionSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/ProtectionSpell"), force, attributes, origin, target);
                    case null:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/EarthSpell"), force, origin, target, false);
                    default:
                        return null;
                }
            case EnumElement.Fire:
                switch (element2)
                {
                    case EnumElement.Air:
                        return CreateBurnSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/BurnSpell"), force, attributes, origin, target);
                    case EnumElement.Earth:
                        return CreateLavaSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/LavaSpell"), force, attributes, origin, target);
                    case EnumElement.Fire:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/FireSpell"), force, origin, target, true);
                    case EnumElement.Water:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/FireSpell"), force, origin, target, false);
                    case EnumElement.Light:
                        return CreateClearStatusSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/ClearStatusSpell"), origin);
                    case null:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/FireSpell"), force, origin, target, false);
                    default:
                        return null;
                }
            case EnumElement.Water:
                switch (element2)
                {
                    case EnumElement.Air:
                        return CreateFreezeTimeSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/FreezeSpell"), attributes);
                    case EnumElement.Earth:
                        return CreateRootingSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/RootingSpell"), force, attributes, origin, target);
                    case EnumElement.Fire:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/WaterSpell"), force, origin, target, false);
                    case EnumElement.Water:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/WaterSpell"), force, origin, target, true);
                    case EnumElement.Light:
                        return CreateHealthSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/CureSpell"), force, attributes, origin);
                    case null:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/WaterSpell"), force, origin, target, false);
                    default:
                        return null;
                }
            case EnumElement.Light:
                switch (element2)
                {
                    case EnumElement.Air:
                        return CreateTimeSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/AddTimeSpell"), attributes);
                    case EnumElement.Earth:
                        return CreateProtectionSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/ProtectionSpell"), force, attributes, origin, target);
                    case EnumElement.Fire:
                        return CreateClearStatusSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/ClearStatusSpell"), origin);
                    case EnumElement.Water:
                        return CreateHealthSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/CureSpell"), force, attributes, origin);
                    case EnumElement.Light:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/LightSpell"), force, origin, target, false);
                    case null:
                        return CreateAttackSpell(prefabSpell, CustomResources.Load<Sprite>("Spells/LightSpell"), force, origin, target, false);
                    default:
                        return null;
                }
            default:
                return null;
        }
    }
    #endregion

    #region CreateSpells
    private static ISpell CreateAttackSpell(GameObject prefabSpell, Sprite sprite, int force, Character origin, Character target, bool critical)
    {
        var spell = prefabSpell.AddComponent<AttackSpell>();
        spell.Initialize(force, origin, target, critical, null, sprite);
        return spell;
    }

    private static ISpell CreateBurnSpell(GameObject prefabSpell, Sprite sprite, int force, AttributePoints playerAttributes, Character origin, Character target)
    {
        var spell = prefabSpell.AddComponent<AttackSpell>();
        var status = CreateBurnStatus(playerAttributes);
        spell.Initialize(force, origin, target, false, status, sprite);
        return spell;
    }

    private static ISpell CreateFreezeTimeSpell(GameObject prefabSpell, Sprite sprite, AttributePoints playerAttributes)
    {
        var spell = prefabSpell.AddComponent<FreezeTimeSpell>();        
        var time = (int)Math.Floor((decimal)((playerAttributes.Water + playerAttributes.Air) / 2));
        spell.Initialize(time, sprite);
        return spell;
    }

    private static ISpell CreateTimeSpell(GameObject prefabSpell, Sprite sprite, AttributePoints playerAttributes)
    {
        var spell = prefabSpell.AddComponent<TimeSpell>();
        var time = (int)Math.Floor((decimal)((playerAttributes.Light + playerAttributes.Air) / 2));
        spell.Initialize(time, sprite);
        return spell;
    }

    private static ISpell CreateLavaSpell(GameObject prefabSpell, Sprite sprite, int force, AttributePoints playerAttributes, Character origin, Character target)
    {
        var spell = prefabSpell.AddComponent<AttackSpell>();
        var status = CreateBurnStatus(playerAttributes);                       
        spell.Initialize(force, origin, target, false, status, sprite);

        return spell;
    }

    private static ISpell CreateRootingSpell(GameObject prefabSpell, Sprite sprite, int force, AttributePoints playerAttributes, Character origin, Character target)
    {
        var spell = prefabSpell.AddComponent<AttackSpell>();
        var status = CreateRootingStatus(playerAttributes);        
        spell.Initialize(force, origin, target, false, status, sprite);

        return spell;
    }

    private static ISpell CreateHealthSpell(GameObject prefabSpell, Sprite sprite, int force, AttributePoints playerAttributes, Character origin)
    {
        var spell = prefabSpell.AddComponent<HealthSpell>();
        spell.Initialize(sprite, (int)Math.Floor((decimal)(force / 2)), (int)Math.Floor((decimal)((playerAttributes.Light + playerAttributes.Water) / 2)), origin);
        return spell;
    }

    private static ISpell CreateClearStatusSpell(GameObject prefabSpell, Sprite sprite, Character origin)
    {
        var spell = prefabSpell.AddComponent<ClearStatusSpell>();
        spell.Initialize(sprite, origin);

        return spell;
    }

    private static ISpell CreateProtectionSpell(GameObject prefabSpell, Sprite sprite, int force, AttributePoints playerAttributes, Character origin, Character target)
    {
        var spell = prefabSpell.AddComponent<ProtectionSpell>();
        spell.Initialize(force, sprite, origin, target);
        return spell;
    }
    #endregion

    #region CreateStatus
    private static IStatus CreateBurnStatus(AttributePoints playerAttributes)
    {
        var statusPrefab = CreateStatusPrefab();
        var status = statusPrefab.AddComponent<BurnStatus>();
        status.Initialize((int)Math.Ceiling((decimal)(playerAttributes.Fire / 2)), (int)Math.Ceiling((decimal)(playerAttributes.Fire / 2)));                

        return status;
    }

    private static IStatus CreateRootingStatus(AttributePoints playerAttributes)
    {
        var statusPrefab = CreateStatusPrefab();
        var status = statusPrefab.AddComponent<RootingStatus>();
        status.Initialize((int)Math.Floor((decimal)((playerAttributes.Water + playerAttributes.Earth) / 2)));        

        return status;
    }
    #endregion

    #region CalculateSpellForce
    private static int CalculateSpellForce(List<Runa> selectedRunas, AttributePoints playerAttributes)
    {
        EnumElement element = selectedRunas[0].Element;

        int elementForce = 0;

        if (element == EnumElement.Air)
            elementForce = playerAttributes.Air;
        else if (element == EnumElement.Earth)
            elementForce = playerAttributes.Earth;
        else if (element == EnumElement.Fire)
            elementForce = playerAttributes.Fire;
        else if (element == EnumElement.Light)
            elementForce = playerAttributes.Light;
        else if (element == EnumElement.Water)
            elementForce = playerAttributes.Water;

        var force = 0;

        var runasBuff = selectedRunas.Count(x => x.PoweredUp);
        var runasNormal = selectedRunas.Count(x => !x.PoweredUp);

        force = (int)(elementForce * (runasBuff * 5) * 1.5) + (elementForce * runasNormal * 5 );
        
        return force;
    }
    #endregion

    #region CreatePrefabs
    private static GameObject CreateSpellPrefab()
    {
        return Instantiate(CustomResources.Load<GameObject>("Spells/SpellPrefab"));
        
    }

    private static GameObject CreateStatusPrefab()
    {
        return Instantiate(CustomResources.Load<GameObject>("Status/StatusPrefab"));
    }
    #endregion

    #region GetPreviewSpellType
    public static CreateSpellType GetPreviewSpellType(EnumElement? comboStoneElement, EnumElement currentSelectedElement)
    {
        if (comboStoneElement == null)
            return CreateSpellType.Attack;

        switch (comboStoneElement)
        {
            case EnumElement.Air:
                switch (currentSelectedElement)
                {
                    case EnumElement.Air: return CreateSpellType.Attack;
                    case EnumElement.Earth: return CreateSpellType.Attack;
                    case EnumElement.Fire: return CreateSpellType.Attack;
                    case EnumElement.Water: return CreateSpellType.FreezeTime;
                    case EnumElement.Light: return CreateSpellType.Time;
                    default: throw new Exception("Combinação de elementos não encontrada");
                }
            case EnumElement.Earth:
                switch (currentSelectedElement)
                {
                    case EnumElement.Air: return CreateSpellType.Attack;
                    case EnumElement.Earth: return CreateSpellType.Attack;
                    case EnumElement.Fire: return CreateSpellType.Attack;
                    case EnumElement.Water: return CreateSpellType.Attack;
                    case EnumElement.Light: return CreateSpellType.Protection;
                    default: throw new Exception("Combinação de elementos não encontrada");
                }
            case EnumElement.Fire:
                switch (currentSelectedElement)
                {
                    case EnumElement.Air: return CreateSpellType.Attack;
                    case EnumElement.Earth: return CreateSpellType.Attack;
                    case EnumElement.Fire: return CreateSpellType.Attack;
                    case EnumElement.Water: return CreateSpellType.Attack;
                    case EnumElement.Light: return CreateSpellType.Clear;
                    default: throw new Exception("Combinação de elementos não encontrada");
                }
            case EnumElement.Water:
                switch (currentSelectedElement)
                {
                    case EnumElement.Air: return CreateSpellType.FreezeTime;
                    case EnumElement.Earth: return CreateSpellType.Attack;
                    case EnumElement.Fire: return CreateSpellType.Attack;
                    case EnumElement.Water: return CreateSpellType.Attack;
                    case EnumElement.Light: return CreateSpellType.Health;
                    default: throw new Exception("Combinação de elementos não encontrada");
                }
            case EnumElement.Light:
                switch (currentSelectedElement)
                {
                    case EnumElement.Air: return CreateSpellType.Time;
                    case EnumElement.Earth: return CreateSpellType.Protection;
                    case EnumElement.Fire: return CreateSpellType.Clear;
                    case EnumElement.Water: return CreateSpellType.Health;
                    case EnumElement.Light: return CreateSpellType.Attack;
                    default: throw new Exception("Combinação de elementos não encontrada");
                }
            default: throw new Exception("Combinação de elementos não encontrada");
        }
    }
    #endregion


}
