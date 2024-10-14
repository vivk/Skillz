using System;
using Features.Abilities.Core;
using Features.Actors.Dependencies;
using Features.Blackbox.Core;
using Features.Health.Core;
using Features.Health.Statuses;
using UnityEngine;

namespace Features.Actors.Components
{
    [Serializable]
    public class CastAreaDamageDealer : IComponent
    {
        public int Damage;
        public HitableType Type;
        
        [Space]
        public StatusType StatusType;
        public byte StatusLevel;
        
        [Space]
        public float Radius;
        public Vector3 Offset;
        public LayerMask Mask;
        
        private ActorStateDependency _actorStateDependency;
        
        private Collider[] _results = new Collider[10];
     
        public void Inject(IBlackboxContainer blackbox)
        {
            _actorStateDependency = blackbox.GetElement<ActorStateDependency>();
        }

        public void Execute()
        {
            var self = _actorStateDependency.Hitable;
            var origin = _actorStateDependency.Origin;
            var position = origin.position + Offset;
            var size = Physics.OverlapSphereNonAlloc(position, Radius, _results, Mask);
            for (var i = 0; i < size; i++)
            {
                var transform = _results[i].transform;
                if (transform == self)
                {
                    continue;
                }
                var ok = HitableContainer.TryGet(transform, out var hitable);
                if (!ok)
                {
                    continue;
                }
                hitable.Hit(Type, Damage);
                hitable.SetStatus(StatusType, StatusLevel);
            }
        }

        public void Tick(float deltaTime)
        {
            
        }

        public object Clone()
        {
            return new CastAreaDamageDealer
            {
                Damage = Damage,
                Type = Type,
                StatusType = StatusType,
                StatusLevel = StatusLevel,
                Radius = Radius,
                Offset = Offset,
                Mask = Mask
            };
        }
    }
}
