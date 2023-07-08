using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class HeroMove : MonoBehaviour
    {
        public float RotationSensitivity;
        public float MovementSpeed;
        public CharacterController CharacterController;

        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        private void Rotate()
        {
            var rotation = _inputService.RotationAxis;
            rotation.x = 0;
            rotation *= RotationSensitivity;
            transform.Rotate(rotation);
        }

        private void Move()
        {
            Vector3 movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y);
                movementVector = transform.TransformDirection(movementVector);
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            CharacterController.Move(movementVector * (MovementSpeed * Time.deltaTime));
        }
    }
}