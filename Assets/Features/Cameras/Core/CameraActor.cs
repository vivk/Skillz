using UnityEngine;

namespace Features.Camera
{
    public class CameraActor : MonoBehaviour
    {
        public Transform CameraTransform;
        public Transform PlayerTransform;
        
        public Vector3 CameraOffset;
        public Vector3 CameraRotation;
        
        private void LateUpdate()
        {
            CameraTransform.position = PlayerTransform.position + CameraOffset;
            CameraTransform.eulerAngles = CameraRotation;
        }
    }
}
