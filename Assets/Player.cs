using DefaultNamespace;
using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private Transform _cc;

    private void Awake()
    {
        _cc = GetComponent<Transform>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.Direction.Normalize();
            _cc.localPosition +=(5*data.Direction*Runner.DeltaTime);
        }
    }
}
