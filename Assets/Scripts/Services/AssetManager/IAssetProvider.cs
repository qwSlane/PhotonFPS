using UnityEngine;

namespace DefaultNamespace.Services
{
    public interface IAssetProvider : IService
    {
        T Asset<T>(string path) where T : Object;
    }
}