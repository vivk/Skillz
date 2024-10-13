using System;
using Features.Abilities;
using Features.Abilities.Core;
using Features.Actors.Dependencies;
using Features.Blackbox;
using Features.Blackbox.Core;

namespace Features.Actors.Components
{
    [Serializable]
    public class CastAnimationPlayer : IComponent
    {
        public string AnimationName = "Cast";
        
        private AnimatorDependency _animatorDependency;
        
        public void Inject(IBlackboxContainer blackbox)
        {
            _animatorDependency = blackbox.GetElement<AnimatorDependency>();
        }

        public void Execute()
        {
            _animatorDependency.SetAnimation(AnimationName);
        }

        public void Tick(float deltaTime)
        {
            
        }

        public object Clone()
        {
            return new CastAnimationPlayer
            {
                AnimationName = AnimationName
            };
        }
    }
}
