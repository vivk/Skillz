using UnityEngine;

namespace Features.Abilities.Core
{
    [CreateAssetMenu(menuName = "Abilities/Spell", fileName = "New Spell")]
    public class SpellHolder : ScriptableObject
    {
        public Spell Spell;
        
        public Spell Get()
        {
            return Spell.Clone() as Spell;
        }
    }
}
