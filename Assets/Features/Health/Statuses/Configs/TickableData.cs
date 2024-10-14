using System;
using Features.Health.Core;
using UnityEngine;

namespace Features.Health.Statuses.Configs
{
    [Serializable]
    internal class TickableData : ICloneable
    {
        public int Damage;
        public HitableType Type;
        public float Duration;
        public float TickInterval;
        
        [HideInInspector]
        public float CurrentTickInterval;
        [HideInInspector]
        public float CurrentDuration;

        public object Clone()
        {
            return new TickableData
            {
                Damage = Damage,
                Type = Type,
                Duration = Duration,
                TickInterval = TickInterval,
                
                CurrentTickInterval = 0,
                CurrentDuration = 0,
            };
        }
    }
}
