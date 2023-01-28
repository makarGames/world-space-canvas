using UnityEngine;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(Canvas))]
    public class WorldSpaceCanvas : MonoBehaviour
    {
        [SerializeField] private bool _lookAtCamera = true;

        private Transform _cashedTransform;
        private Transform _mainCameraTransform;
        private Camera _mainCamera;

        private void Awake()
        {
            _cashedTransform = transform;
            _mainCamera = Camera.main;
            
            if (_mainCamera == null)
            {
                Debug.LogWarning("Main Camera not found!");
                return;
            }
            
            _mainCameraTransform = _mainCamera.transform;

            if (TryGetComponent<Canvas>(out var canvas))
            {
                if (canvas.renderMode == RenderMode.WorldSpace)
                {
                    canvas.worldCamera = _mainCamera;
                }
            }
        }

        private void Update()
        {
            if (_lookAtCamera)
            {
                LookAtCamera();
            }
        }

        private void LookAtCamera()
        {
            var rotation = _mainCameraTransform.rotation;

            var worldPosition = _cashedTransform.position + rotation * Vector3.forward;
            var worldUp = rotation * Vector3.up;

            _cashedTransform.LookAt(worldPosition, worldUp);
        }
    }
}