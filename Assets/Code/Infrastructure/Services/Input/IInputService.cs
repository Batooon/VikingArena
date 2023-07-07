using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackPressed();
    }
}