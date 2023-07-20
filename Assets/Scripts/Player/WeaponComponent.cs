using DefaultNamespace;
using DefaultNamespace.Player;
using Fusion;
using UnityEngine;

public class WeaponComponent : NetworkBehaviour
{
    [SerializeField] private Bullet Bullet;

    [SerializeField] private Player Player;

    [Networked(OnChanged = nameof(OnFireChanged))]
    public bool isFiring { get; set; }

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

        Bullet bullet = Runner.Spawn(Bullet, transform.localPosition, Quaternion.identity, Runner.LocalPlayer);
        bullet.Init(directionVector, Player.Hitbox);
        lastTimeFired = Time.time;
    }

    static void OnFireChanged(Changed<WeaponComponent> changed) { }
}