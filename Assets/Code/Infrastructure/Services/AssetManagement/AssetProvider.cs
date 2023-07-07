using UnityEngine;

namespace Code.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(GameObject prefab, Vector3 at) =>
            Object.Instantiate(prefab, at, Quaternion.identity);

        public GameObject Instantiate(GameObject prefab) =>
            Object.Instantiate(prefab);

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Load(path);
            return Instantiate(prefab, at);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Load(path);
            return Instantiate(prefab);
        }

        private GameObject Load(string path) =>
            Resources.Load<GameObject>(path);
    }
}