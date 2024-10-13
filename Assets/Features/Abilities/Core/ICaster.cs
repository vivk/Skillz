using Features.Blackbox.Core;

namespace Features.Abilities.Core
{
    public interface ICaster
    {
        public void Inject(IBlackboxContainer blackbox);
        
        public void Execute();
        
        public void Tick(float deltaTime);
    }
}
