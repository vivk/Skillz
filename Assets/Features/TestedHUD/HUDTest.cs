using Features.Actors.Dependencies;
using Features.Blackbox;
using Features.Blackbox.Core;
using TMPro;
using UnityEngine;

namespace Features.Health.Test
{
    public class HUDTest : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public HitRegistrator HitRegistrator;
        public BlackboxContainer BlackboxContainer;
        
        private ActorStatsDependency _actorStatsDependency;
        
        private void Start()
        {
            _actorStatsDependency = BlackboxContainer.GetElement<ActorStatsDependency>();
        }
        
        private void Update()
        {
            Text.text = $"Health: {HitRegistrator.CurrentHealth}/{HitRegistrator.FullHealth}\nArmor: {_actorStatsDependency.Armor.Value}";
        }
    }
}
