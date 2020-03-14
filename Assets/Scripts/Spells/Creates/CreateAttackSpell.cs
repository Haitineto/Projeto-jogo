using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Spells.Creates
{
    public class CreateAttackSpell : MonoBehaviour, ICreateSpell
    {
        public CreateSpellType Type { get; set; }
        public Sprite Sprite { get; set; }
        public int Force { get; set; }
        public Character Origin { get; set; }
        public Character Target { get; set; }
        public bool Critical { get; set; }
        public AttributePoints AttributePoints { get; set; }

        public void Create(Sprite sprite, int force, Character origin, Character target, bool critical, AttributePoints attributePoints)
        {
            Type = CreateSpellType.Attack;
            Sprite = sprite;
            Force = force;
            Origin = origin;
            Target = target;
            Critical = critical;
            AttributePoints = attributePoints;
        }

        public void Execute()
        {
            var prefab = Instantiate(CustomResources.Load<GameObject>("Spells/SpellPrefab"));
            var spell = prefab.AddComponent<AttackSpell>();
            spell.Initialize(Force, Origin, Target, Critical, null, Sprite);
            spell.Execute();
        }
    }
}
