using System;
using Features.Abilities;
using Features.Abilities.Core;
using Features.Actors.Dependencies;
using Features.Blackbox;
using Features.Blackbox.Core;

namespace Features.Actors.Components
{
    [Serializable]
    public class CastParticlePlayer : IComponent
    {
        public int ID = 0;
        public float Duration = 1;
        
        private ParticlePlayerDependency _particlePlayerDependency;
        
        public void Inject(IBlackboxContainer blackbox)
        {
            _particlePlayerDependency = blackbox.GetElement<ParticlePlayerDependency>(ID);
        }

        public void Execute()
        {
            _particlePlayerDependency.SetDuration(Duration);
            _particlePlayerDependency.Play();
        }

        public void Tick(float deltaTime)
        {
            
        }

        public object Clone()
        {
            return new CastParticlePlayer
            {
                ID = ID,
                Duration = Duration,
            };
        }
    }
}
