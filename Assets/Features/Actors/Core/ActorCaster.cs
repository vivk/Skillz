using Features.Abilities.Core;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Core
{
    public class ActorCaster : MonoBehaviour
    {
        public SpellHolder SpellSpaceHolder;
        public SpellHolder Spell1Holder;
        public SpellHolder Spell2Holder;
     
        [Space]
        public bool IsPlayer;
        
        [Space]
        public BlackboxContainer BlackboxContainer;

        private Spell _spellSpace;
        private Spell _spell1;
        private Spell _spell2;
        
        public void CastSpellSpace()
        {
            _spellSpace.Execute();
        }
        
        public void CastSpell1()
        {
            _spell1.Execute();
        }
        
        public void CastSpell2()
        {
            _spell2.Execute();
        }
        
        private void Start()
        {
            _spellSpace = SpellSpaceHolder.Get();
            _spellSpace.Inject(BlackboxContainer);
            
            _spell1 = Spell1Holder.Get();
            _spell1.Inject(BlackboxContainer);
            
            _spell2 = Spell2Holder.Get();
            _spell2.Inject(BlackboxContainer);
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;

            if (_spellSpace != null)
            {
                _spellSpace.Tick(deltaTime);
            }
            
            if (_spell1 != null)
            {
                _spell1.Tick(deltaTime);
            }
            
            if (_spell2 != null)
            {
                _spell2.Tick(deltaTime);
            }

            if (IsPlayer)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CastSpellSpace();
                }
            
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    CastSpell1();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    CastSpell2();
                }
            }
        }
    }
}
