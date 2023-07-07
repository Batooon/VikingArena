using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string HorizontalRotation = "Mouse X";
        protected const string VerticalRotation = "Mouse Y";
        private const string Attack = "Fire";
        
        public abstract Vector2 Axis { get; }
        public abstract Vector2 RotationAxis { get; }

        public bool IsAttackPressed() => 
            UnityEngine.Input.GetButtonDown(Attack);
    }
}