using System;
using Features.Actors.Dependencies;
using Features.Blackbox;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Health
{
    public class HitRegistrator : MonoBehaviour, IHitable
    {
        [SerializeField]
        private int fullHealth = 100;

        [Space] 
        [SerializeField] private Transform _transform;
        
        [Space]
        [SerializeField]
        private BlackboxContainer blackboxContainer;
        
        private ActorStatsDependency _actorStatsDependency;
        
        private int _currentHealth;
        
        public int FullHealth => fullHealth;
        public int CurrentHealth => _currentHealth;
        
        private void ResetHealth()
        {
            _currentHealth = fullHealth;
        }

        private void Start()
        {
            _actorStatsDependency = blackboxContainer.GetElement<ActorStatsDependency>();
            
            ResetHealth();
            
            HitableContainer.Subscribe(_transform, this);
        }
        
        private void OnDestroy()
        {
            HitableContainer.Unsubscribe(_transform);
        }

        public void Hit(int damage)
        {
            var armor = _actorStatsDependency.Armor.Value;
            damage = damage - armor;
            damage = damage < 1 ? 1 : damage;
            
            _currentHealth = _currentHealth - damage;

            if (_currentHealth > 0)
            {
                return;
            }
            
            Debug.Log("Dead");
                
            ResetHealth();
        }
    }
}
