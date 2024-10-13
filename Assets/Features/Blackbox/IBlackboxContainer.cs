using System.Collections.Generic;

namespace Features.Blackbox
{
    public interface IBlackboxContainer
    {
        List<IBlackboxContainer> Dependencies { get; }

        public IBlackboxElement GetElement<T>(int id = 0) where T : ABlackboxElement;

        public void Subscribe(IBlackboxContainer container);
        public void Unsubscribe(IBlackboxContainer container);
    }
}
