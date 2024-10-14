using System.Collections.Generic;
using Features.Health.Core;
using Features.Health.Statuses.Configs;
using UnityEngine;

namespace Features.Health.Statuses
{
    public class StatusController : MonoBehaviour
    {
        [SerializeField] 
        private HitRegistrator _hitRegistrator;
        
        [Header("Dependencies")]
        [SerializeField]
        private StatusConfig _config;

        private Dictionary<StatusType, TickableData> _tickableStatuses;
        private List<StatusType> _toRemoveTickableStatusTypes;

        private void Start()
        {
            _tickableStatuses = new Dictionary<StatusType, TickableData>();
            _toRemoveTickableStatusTypes = new List<StatusType>();
            
            _hitRegistrator.OnStatusSet += OnStatusSet;
        }
        
        private void OnDestroy()
        {
            _hitRegistrator.OnStatusSet -= OnStatusSet;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            
            _toRemoveTickableStatusTypes.Clear();
            
            foreach (var tickableStatus in _tickableStatuses)
            {
                UpdateTickableStatus(tickableStatus, deltaTime);
            }

            for (var i = 0; i < _toRemoveTickableStatusTypes.Count; i++)
            {
                var type = _toRemoveTickableStatusTypes[i];
                var contains = _tickableStatuses.ContainsKey(type);
                if (!contains)
                {
                    continue;
                }
                _tickableStatuses.Remove(type);
            }
        }
        
        private void UpdateTickableStatus(KeyValuePair<StatusType, TickableData> tickableStatus, float deltaTime)
        {
            var type = tickableStatus.Key;
            var status = tickableStatus.Value;
            
            status.CurrentDuration += deltaTime;
            status.CurrentTickInterval += deltaTime;
            
            if (status.CurrentTickInterval > status.TickInterval)
            {
                status.CurrentTickInterval -= status.TickInterval;
                _hitRegistrator.Hit(status.Type, status.Damage); 
            }
            
            if (status.CurrentDuration > status.Duration)
            {
                _toRemoveTickableStatusTypes.Add(type);
            }
        }

        private void OnStatusSet(StatusType type, byte level)
        {
            switch (type)
            {
                case StatusType.None:
                    return;
                case StatusType.Bleed:
                    var bleedData = _config.GetBleedData(level);
                    _tickableStatuses[type] = bleedData;
                    break;
                default:
                    Debug.LogError($"Status {type} is not implemented", this);
                    return;
            }
        }
    }
}
