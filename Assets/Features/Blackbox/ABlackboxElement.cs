using UnityEngine;

namespace Features.Blackbox
{
    public class ABlackboxElement : MonoBehaviour, IBlackboxElement
    {
        [Header("Blackbox Element")]
        [SerializeField] private int _id;
        
        public int ID => _id;
        
        public bool EqualType<T>() where T : IBlackboxElement
        {
            return this is T;
        }
    }
}
