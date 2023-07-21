using DefaultNamespace;
using DefaultNamespace.Player;
using Fusion;
using UnityEngine;

public class WeaponComponent : NetworkBehaviour
{
    [SerializeField] private Bullet Bullet;
    [SerializeField] private Player Player;

    private float lastTimeFired = 0.15f;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData inputData))
        {
            if (inputData.IsFireUp)
            {
                Fire(Player.Forward);
            }
        }
    }

    private void Fire(Vector2 directionVector)
    {
        if (Time.time - lastTimeFired < 0.15f)
        {
            return;
        }

        Runner.Spawn(Bullet, transform.localPosition, Quaternion.identity, Object.InputAuthority, (runner, spawned) =>
        {
          spawned.GetComponent<Bullet>().Init(directionVector, Player.Hitbox);  
        });
        lastTimeFired = Time.time;
    }

}