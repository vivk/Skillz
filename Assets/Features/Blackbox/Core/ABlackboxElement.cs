using UnityEngine;

namespace Features.Blackbox.Core
{
    public class ABlackboxElement : MonoBehaviour, IBlackboxElement
    {
        [SerializeField] 
        private int id;
        
        public int ID => id;
        
        public bool EqualType<T>() where T : IBlackboxElement
        {
            return this is T;
        }
    }
}
