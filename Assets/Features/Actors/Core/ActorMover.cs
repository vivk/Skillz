using Features.Actors.Dependencies;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Core
{
    public class ActorMover : MonoBehaviour
    {
        public Rigidbody Rb;
        public Vector3 Offset;
        
        [Space]
        public BlackboxContainer Blackbox;
        
        private ActorStateDependency _actorState;
        
        private void Start()
        {
            _actorState = Blackbox.GetElement<ActorStateDependency>();
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;

            var isInput = _actorState.IsInput;
            var moveSpeed = _actorState.MovementSpeed;
            
            var inputVelocity = GetInputVelocity(isInput, moveSpeed);
            inputVelocity = inputVelocity * deltaTime;
            
            var externalVelocity = _actorState.ExternalVelocity;
            externalVelocity = externalVelocity * deltaTime;
            
            Rb.linearVelocity = inputVelocity + externalVelocity;
        }
        
        private Vector3 GetInputVelocity(bool isInput, float speed)
        {
            var input = GetInput(isInput);
            
            input = Quaternion.Euler(Offset) * input;
            input = speed * input;

            return input;
        }
        
        private Vector3 GetInput(bool isInput)
        {
            var input = Vector3.zero;
            
            if (!isInput)
            {
                return input;
            }
            
            if (Input.GetKey(KeyCode.W))
            {
                input.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                input.z -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                input.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                input.x += 1;
            }

            return input.normalized;
        }
    }
}
