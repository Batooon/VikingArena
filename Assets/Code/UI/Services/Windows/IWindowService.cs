using System;
using Code.Infrastructure.Services;
using Code.Infrastructure.Services.StaticData;
using Code.UI.Services.Factory;

namespace Code.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
    }
}