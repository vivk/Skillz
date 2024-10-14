using System.Collections.Generic;
using UnityEngine;

namespace Features.Health.Core
{
    public static class HitableContainer
    {
        private static readonly Dictionary<Transform, IHitable> _hitables = new();
        
        public static void Subscribe(Transform transform, IHitable hitable)
        {
            var contains = _hitables.ContainsKey(transform);
            if (contains)
            {
                _hitables[transform] = hitable;
            }
            else
            {
                _hitables.Add(transform, hitable);
            }
        }
        
        public static void Unsubscribe(Transform transform)
        {
            var contains = _hitables.ContainsKey(transform);
            if (!contains)
            {
                return;
            }
            _hitables.Remove(transform);
        }
        
        public static bool TryGet(Transform transform, out IHitable hitable)
        {
            return _hitables.TryGetValue(transform, out hitable);
        }
    }
}
