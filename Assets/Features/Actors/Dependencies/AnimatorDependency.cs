using Features.Blackbox;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Dependencies
{
    public class AnimatorDependency : ABlackboxElement
    {
        public Animator Animator;
        
        public void SetAnimation(string animationName)
        {
            Animator.Play(animationName);
        }
    }
}
