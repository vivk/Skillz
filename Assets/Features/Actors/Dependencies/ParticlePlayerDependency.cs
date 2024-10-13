using Features.Blackbox;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Dependencies
{
    public class ParticlePlayerDependency : ABlackboxElement
    {
        [SerializeField]
        private ParticleSystem _particleSystem;
        
        public void Play()
        {
            _particleSystem.Play();
        }
        
        public void SetDuration(float duration)
        {
            var main = _particleSystem.main;
            main.duration = duration;
        }
    }
}
