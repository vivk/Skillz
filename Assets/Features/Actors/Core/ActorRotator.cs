using Features.Actors.Dependencies;
using Features.Blackbox.Core;
using UnityEngine;

namespace Features.Actors.Core
{
    public class ActorRotator : MonoBehaviour
    {
        public Transform Origin;
        public Vector3 Offset;
        
        [Space]
        public BlackboxContainer Blackbox;
        
        private ActorStateDependency _actorState;
        
        private UnityEngine.Camera _camera;
        
        private void Start()
        {
            _actorState = Blackbox.GetElement<ActorStateDependency>();
            
            _camera = UnityEngine.Camera.main;
        }
        
        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            
            var isInput = _actorState.IsInput;
            
            Quaternion rotation;
            
            if (!isInput)
            {
                rotation = _actorState.LookRotation;
            }
            else
            {
                var mousePosition = GetMousePosition();
            
                var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
                rotation = Quaternion.AngleAxis(angle, Vector3.down);
                rotation.eulerAngles += Offset;
                
                _actorState.LookRotation = rotation;
            }
            
            var rotateSpeed = _actorState.RotationSpeed;
            Origin.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * deltaTime);
        }
        
        private Vector3 GetMousePosition()
        {
            var mousePosition = Input.mousePosition;
            mousePosition -= _camera.WorldToScreenPoint(Origin.position);
            
            return mousePosition;
        }
    }
}
