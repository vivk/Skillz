using System;
using Features.Abilities;
using Features.Abilities.Core;
using Features.Actors.Dependencies;
using Features.Blackbox;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Components
{
    [Serializable]
    public class CastMoveLookDirection : IComponent
    {
        public float MoveSpeed = 450;
        public float Duration = 1f;
        
        [Space]
        public bool ReverseDirection;
        
        private ActorStateDependency _actorState;
        
        private Vector3 _direction;
        
        private float _currentDuration;
        
        private bool _isRunning;
        
        public void Inject(IBlackboxContainer blackbox)
        {
            _actorState = blackbox.GetElement<ActorStateDependency>();
        }

        public void Execute()
        {
            _currentDuration = Duration;
            
            var lookAngle = _actorState.LookRotation.eulerAngles.y;
            var radian = lookAngle * Mathf.Deg2Rad;
            var direction = new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian)).normalized;
            _direction = ReverseDirection 
                ? -direction 
                :  direction;
            
            _actorState.ExternalVelocity = _direction * MoveSpeed;
            _actorState.IsInput = false;
            
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
            
            _actorState.ExternalVelocity = Vector3.zero;
            _actorState.IsInput = true;
                
            _isRunning = false;
        }

        public object Clone()
        {
            return new CastMoveLookDirection
            {
                MoveSpeed = MoveSpeed,
                Duration = Duration,
                ReverseDirection = ReverseDirection
            };
        }
    }
}
