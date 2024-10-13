using System;
using Features.Blackbox.Core;
using Features.SelectableInterface.Attributes;
using UnityEngine;

namespace Features.Abilities.Core
{
    [Serializable]
    public class Spell : ICaster, ICloneable
    {
        public float Cooldown;

        [Space] 
        [SelectableImpl, SerializeReference]
        public IComponent[] Components;
        
        private float _currentCooldown;

        public void Inject(IBlackboxContainer blackbox)
        {
            for (var i = 0; i < Components.Length; i++)
            {
                var component = Components[i];
                component.Inject(blackbox);
            }
        }

        public void Execute()
        {
            if (!IsReady())
            {
                return;
            }
            
            for (var i = 0; i < Components.Length; i++)
            {
                var component = Components[i];
                component.Execute();
            }
            
            _currentCooldown = Cooldown;
        }

        public void Tick(float deltaTime)
        {
            if (!IsReady())
            {
                _currentCooldown -= deltaTime;
            }
            
            for (var i = 0; i < Components.Length; i++)
            {
                var component = Components[i];
                component.Tick(deltaTime);
            }
        }

        public object Clone()
        {
            var spell = new Spell
            {
                Cooldown = Cooldown,
                Components = new IComponent[Components.Length]
            };

            for (var i = 0; i < Components.Length; i++)
            {
                var component = Components[i];
                spell.Components[i] = (IComponent) component.Clone();
            }

            return spell;
        }
        
        private bool IsReady()
        {
            return _currentCooldown <= 0;
        }
    }
}

