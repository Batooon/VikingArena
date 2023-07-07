using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis => UnityAxis();

        private Vector2 UnityAxis() => 
            new(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}