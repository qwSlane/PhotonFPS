using Fusion;
using UnityEngine;

namespace DefaultNamespace.Services.Factory
{
    public interface IFactory
    {
        public T Create<T>(string path, Vector3 at) where T : Object;
        public NetworkObject CreateNetObject<T>(string path, Vector3 at, PlayerRef player) where T : NetworkObject;
    }
}