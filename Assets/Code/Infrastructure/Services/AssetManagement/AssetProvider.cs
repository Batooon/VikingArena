using UnityEngine;

namespace Code.Infrastructure.Services.AssetManagement
{
    public static class AssetProvider
    {
        public static GameObject Instantiate(GameObject prefab, Vector3 at) =>
            Object.Instantiate(prefab, at, Quaternion.identity);

        public static GameObject Instantiate(GameObject prefab) =>
            Object.Instantiate(prefab);

        public static GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Load(path);
            return Instantiate(prefab, at);
        }

        public static GameObject Instantiate(string path)
        {
            var prefab = Load(path);
            return Instantiate(prefab);
        }

        private static GameObject Load(string path) =>
            Resources.Load<GameObject>(path);
    }
}