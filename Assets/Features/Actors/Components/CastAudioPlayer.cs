using System;
using Features.Abilities;
using Features.Abilities.Core;
using Features.Actors.Dependencies;
using Features.Blackbox;
using Features.Blackbox.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Actors.Components
{
    [Serializable]
    public class CastAudioPlayer : IComponent
    {
        public AudioClip[] Clips;
        
        private AudioPlayerDependency _audioPlayerDependency;
        
        public void Inject(IBlackboxContainer blackbox)
        {
            _audioPlayerDependency = blackbox.GetElement<AudioPlayerDependency>();
        }

        public void Execute()
        {
            var index = Random.Range(0, Clips.Length);
            var clip = Clips[index];
            
            _audioPlayerDependency.SetClip(clip);
            _audioPlayerDependency.PlayAudio();
        }

        public void Tick(float deltaTime)
        {
        
        }

        public object Clone()
        {
            return new CastAudioPlayer
            {
                Clips = Clips
            };
        }
    }
}
