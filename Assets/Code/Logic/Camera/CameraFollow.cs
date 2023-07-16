using Code.Hero;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Logic.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public float TopRotationLimit;
        public float BottomRotationLimit;
        public float RotationSensitivity;
        public float Distance;
        public float OffsetY;
        public Transform Following;

        private IInputService _inputService;
        private float _rotationX;

        private bool _shouldRotate = true;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _rotationX = transform.eulerAngles.x;
        }

        private void LateUpdate()
        {
            if (CantRotate())
                return;
            if (HasPlayer() == false)
                return;

            Quaternion rotation = Following.rotation * Quaternion.Euler(VerticalRotation(), 0, 0);
            var position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();

            transform.SetPositionAndRotation(position, rotation);
        }

        public void Follow(GameObject following)
        {
            Following = following.transform;
            HeroEventsBus.Died += OnPlayerDeath;
        }

        private bool CantRotate() =>
            _shouldRotate == false;

        private bool HasPlayer() => 
            Following != null;

        private void OnPlayerDeath()
        {
            HeroEventsBus.Died -= OnPlayerDeath;
            _shouldRotate = false;
        }

        private float VerticalRotation()
        {
            _rotationX -= _inputService.RotationAxis.x * RotationSensitivity;
            _rotationX %= 360;
            _rotationX = Mathf.Clamp(_rotationX, BottomRotationLimit, TopRotationLimit);
            return _rotationX;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = Following.position;
            followingPosition.y += OffsetY;

            return followingPosition;
        }
    }
}