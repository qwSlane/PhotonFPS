using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Services
{
    public class AssetProvider : IAssetProvider
    {
        private Dictionary<string, Object> _assets = new Dictionary<string, Object>();

        public T Asset<T>(string path) where T : Object
        {
            T asset;
            if (_assets.TryGetValue(path, out Object value))
            {
                asset = (T)_assets[path];
                return asset;
            }

            asset         = Resources.Load<T>(path);
            _assets[path] = asset;

            return asset;
        }
    }
}