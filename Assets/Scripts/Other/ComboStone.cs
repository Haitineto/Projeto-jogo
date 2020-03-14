using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboStone : MonoBehaviour
{
    public EnumElement? Element { get; private set; }
    [SerializeField]
    public List<PreviewSpell> PreviewSpells;

    public void Initialize()
    {
        Element = null;
    }

    public void SetElement(EnumElement? element)
    {
        Element = element;
        if (element==null)
            GetComponent<SpriteRenderer>().sprite = CustomResources.Load<Sprite>("ComboStones/ComboStoneNone");
        else
            GetComponent<SpriteRenderer>().sprite = CustomResources.Load<Sprite>("ComboStones/ComboStone" + element);
    }

    public void ShowCurrentPreviewSpell(EnumElement? currentSelectedElement)
    {
        if (currentSelectedElement == null)
        {
            PreviewSpells.ForEach(p => p.Disable());
            return;
        }

        var type = SpellFactory.GetPreviewSpellType(Element,currentSelectedElement.Value);
        PreviewSpells.FirstOrDefault(p => p.Type == type).Enable();
    }

}
