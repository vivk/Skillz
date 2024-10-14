using Features.Actors.Dependencies;
using Features.Blackbox.Core;
using Features.Health.Core;
using TMPro;
using UnityEngine;

namespace Features.TestedHUD
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
