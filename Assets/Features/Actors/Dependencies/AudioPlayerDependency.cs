using Features.Blackbox;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Dependencies
{
    public class AudioPlayerDependency : ABlackboxElement
    {
        public AudioSource AudioSource;
        
        public void SetClip(AudioClip audioClip)
        {
            AudioSource.clip = audioClip;
        }
        
        public void PlayAudio()
        {
            AudioSource.Play();
        }
    }
}
