using System.Collections.Generic;
using Code.UI.Services.Factory;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/Windows static data")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}