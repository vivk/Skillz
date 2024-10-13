using System;
using Features.Abilities;
using Features.Abilities.Core;
using Features.Actors.Dependencies;
using Features.Blackbox;
using Features.Blackbox.Core;

namespace Features.Actors.Components
{
    [Serializable]
    public class CastStatsModifier : IComponent
    {
        public StatsType Type;
        public float Duration;
        public int Value;
        
        private ActorStatsDependency _actorStatsDependency;
        
        private float _currentDuration;
        
        private bool _isRunning; 

        public enum StatsType : byte
        {
            Armor,
        }
        
        public void Inject(IBlackboxContainer blackbox)
        {
            _actorStatsDependency = blackbox.GetElement<ActorStatsDependency>();
        }

        public void Execute()
        {
            Set(active: true);
            
            _currentDuration = Duration;
            
            _isRunning = true;
        }

        public void Tick(float deltaTime)
        {
            if (!_isRunning)
            {
                return;
            }
            
            _currentDuration -= deltaTime;
            if (_currentDuration > 0)
            {
                return;
            }
            
            Set(active: false);
            _isRunning = false;
        }

        public object Clone()
        {
            return new CastStatsModifier
            {
                Type = Type,
                Duration = Duration,
                Value = Value,
            };
        }
        
        private void Set(bool active)
        {
            switch (Type)
            {
                case StatsType.Armor:
                    if (active)
                    {
                        _actorStatsDependency.Armor.Subscribe(this, Value);
                    }
                    else
                    {
                        _actorStatsDependency.Armor.Unsubscribe(this);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
