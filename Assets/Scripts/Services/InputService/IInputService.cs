using UnityEngine;

namespace DefaultNamespace.Services.InputService
{
    public interface IInputService : IService
    {
        public NetworkInputData GetNetworkInput();
    }
}