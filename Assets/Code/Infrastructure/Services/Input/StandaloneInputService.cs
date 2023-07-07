using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => UnityAxis();
        public override Vector2 RotationAxis => UnityRotation();

        private Vector2 UnityAxis() => 
            new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));

        private Vector2 UnityRotation() =>
            new(UnityEngine.Input.GetAxis(VerticalRotation),
                UnityEngine.Input.GetAxis(HorizontalRotation));
    }
}