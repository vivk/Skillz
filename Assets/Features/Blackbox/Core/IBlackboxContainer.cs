using System.Collections.Generic;

namespace Features.Blackbox.Core
{
    public interface IBlackboxContainer
    {
        List<IBlackboxContainer> Dependencies { get; }

        public T GetElement<T>(int id = 0) where T : ABlackboxElement;

        public void Subscribe(IBlackboxContainer container);
        public void Unsubscribe(IBlackboxContainer container);
    }
}
