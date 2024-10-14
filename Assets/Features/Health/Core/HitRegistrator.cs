using System;
using Features.Actors.Dependencies;
using Features.Blackbox.Core;
using Features.Health.Statuses;
using UnityEngine;

namespace Features.Health.Core
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
        
        public Action<StatusType, byte> OnStatusSet;
        
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

        public void Hit(HitableType type, int damage)
        {
            switch (type)
            {
                case HitableType.None:
                    return;
                case HitableType.Pure:
                    break;
                case HitableType.Physical:
                    var armor = _actorStatsDependency.Armor.Value;
                    damage -= armor;
                    damage = Mathf.Clamp(damage, 1, damage);
                    break;
                default:
                    Debug.LogError($"Hitable type {type} is not implemented", this);
                    return;
            }
            
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                SetDead();
                return;
            }
        }

        public void SetStatus(StatusType type, byte level)
        {
            OnStatusSet?.Invoke(type, level);
        }
        
        private void SetDead()
        {
            Debug.Log("Dead", this);

            ResetHealth();
        }
    }
}
