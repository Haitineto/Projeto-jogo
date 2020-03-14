using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICreateSpell
    {
        CreateSpellType Type { get; set; }
        Sprite Sprite { get; set; }
        int Force { get; set; }
        Character Origin { get; set; }
        Character Target { get; set; }
        bool Critical { get; set; }
        AttributePoints AttributePoints { get; set; }

        void Create(Sprite sprite, int force, Character origin, Character target, bool critical, AttributePoints attributePoints);
        void Execute();
    }
}
