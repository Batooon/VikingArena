using UnityEngine;

namespace Code.Infrastructure.Services.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(GameObject prefab, Vector3 at);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}