using Features.Blackbox;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Dependencies
{
    public class ActorStateDependency : ABlackboxElement
    {
        public Transform Origin;
        public Transform Hitable;
        
        [Space]
        public bool IsInput = true;

        public float MovementSpeed = 150;
        public float RotationSpeed = 8;
        
        public Quaternion LookRotation;
        public Vector3 ExternalVelocity = Vector3.zero;
    }
}
