using Fusion;
using UnityEngine;

namespace DefaultNamespace.Services.Factory
{
    public class GameFactory : IFactory
    {
        private NetworkRunner _runner;
        private IAssetProvider _assetProvider;

        public GameFactory(NetworkRunner runner, IAssetProvider assetProvider)
        {
            _runner   = runner;
            _assetProvider = assetProvider;
        }

        public T Create<T>(string path, Vector3 at) where T: Object
        {
            var asset = _assetProvider.Asset<T>(path);

            return Object.Instantiate(asset, at, Quaternion.identity);
        }
        
        public T Create<T>(string path) where T: Object
        {
            var asset = _assetProvider.Asset<T>(path);

            return Object.Instantiate(asset, Vector3.zero, Quaternion.identity);
        }

        public T CreateNetObject<T>(string path, Vector3 at, PlayerRef player) where T: NetworkObject
        {
            var asset = _assetProvider.Asset<T>(path);

            return _runner.Spawn(asset, at, Quaternion.identity, player) as T;
        }
    }
}