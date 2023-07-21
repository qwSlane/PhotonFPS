using Fusion;
using UnityEngine;

namespace DefaultNamespace.Services.Factory
{
    public interface IFactory : IService
    {
        public T Create<T>(string path, Vector3 at) where T : Object;
        
        public T Create<T>(string path) where T : Object;
        public T CreateNetObject<T>(string path, Vector3 at, PlayerRef player) where T : NetworkObject;
        
        public T CreateNetBehavior<T>(string path, Vector3 at, PlayerRef player) where T : NetworkBehaviour;
        public T CreateNetBehavior<T>(string path, Vector3 at) where T : NetworkBehaviour;
    }
}