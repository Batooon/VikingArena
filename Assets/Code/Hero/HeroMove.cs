using Code.Infrastructure.Services;
using Code.Infrastructure.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class HeroMove : MonoBehaviour
    {
        public float MovementSpeed;
        public CharacterController CharacterController;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
            }

            movementVector += Physics.gravity;

            CharacterController.Move(movementVector * (MovementSpeed * Time.deltaTime));
        }
    }
}