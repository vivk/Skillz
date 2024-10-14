using UnityEngine;

namespace Features.Health.Statuses.Configs
{
    [CreateAssetMenu(menuName = "Configs/Status/Status Config", fileName = "New Status Config")]
    internal class StatusConfig : ScriptableObject
    {
        [SerializeField] 
        private TickableData[] _bleedLevels;
        
        internal TickableData GetBleedData(int level)
        {
            level = Mathf.Clamp(level, 0, _bleedLevels.Length - 1);
            return _bleedLevels[level].Clone() as TickableData;
        }
    }
}
