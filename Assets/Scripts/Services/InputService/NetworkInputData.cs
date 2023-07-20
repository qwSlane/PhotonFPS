using Fusion;
using UnityEngine;

namespace DefaultNamespace
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector3 Direction;

        public NetworkBool IsFireUp;
    }
}