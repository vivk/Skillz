using System.Collections.Generic;
using UnityEngine;

namespace Features.Blackbox.Core
{
    public class BlackboxContainer : MonoBehaviour, IBlackboxContainer
    {
        public ABlackboxElement[] Elements;
        
        public List<IBlackboxContainer> Dependencies { get; } = new();

        public T GetElement<T>(int id = 0) where T : ABlackboxElement
        {
            for (var i = 0; i < Elements.Length; i++)
            {
                var element = Elements[i];
                if (element.ID == id && element.EqualType<T>())
                {
                    return element as T;
                }
            }
            
            for (var i = 0; i < Dependencies.Count; i++)
            {
                var dependency = Dependencies[i];
                var element = dependency.GetElement<T>(id);
                if (element != null)
                {
                    return element;
                }
            }
            
            Debug.LogError($"Element of type {typeof(T)} with ID {id} not found.");
            
            return null;
        }
        
        public void Subscribe(IBlackboxContainer container)
        {
            if (Dependencies.Contains(container))
            {
                return;
            }
            Dependencies.Add(container);
        }
        
        public void Unsubscribe(IBlackboxContainer container)
        {
            if (!Dependencies.Contains(container))
            {
                return;
            }
            Dependencies.Remove(container);
        }
    }
}
