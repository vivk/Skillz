using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Stats.Core
{
    [Serializable]
    public class StatHolder
    {
        [SerializeField]
        private int baseValue;
        
        private readonly Dictionary<object, int> _params = new();
        
        public int Value => baseValue + CalculateParams();
        
        public void Subscribe(object mod, int value)
        {
            _params[mod] = value;
        }
        
        public void Unsubscribe(object mod)
        {
            _params.Remove(mod);
        }
        
        private int CalculateParams()
        {
            return _params.Sum(mod => mod.Value);
        }
    }
}
