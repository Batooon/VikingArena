using UnityEngine;

namespace Code.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        Vector2 RotationAxis { get; }
        bool IsHoldingRun();
        bool IsAttackPressed();
    }
}