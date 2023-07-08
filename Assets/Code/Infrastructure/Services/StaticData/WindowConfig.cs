using System;
using Code.UI.Windows;

namespace Code.Infrastructure.Services.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}