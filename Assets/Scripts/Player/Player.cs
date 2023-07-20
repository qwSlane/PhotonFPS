using System;
using DefaultNamespace;
using DefaultNamespace.Player;
using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using DefaultNamespace.UI;
using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Hitbox _hitbox;
    [SerializeField] private HpHandler HpHandler;
    [SerializeField] private WeaponComponent WeaponComponent;

    private HUD _hud;

    private Vector2 _forward;
    [SerializeField] private Rigidbody2D rb;
    private float movementSpeed = 5f;
    private IFactory _factory;
    private const float eps = 0.05f;

    public Hitbox Hitbox => _hitbox;
    public Vector2 Forward => _forward;

    public override void Spawned()
    {
        base.Spawned();

        _factory = Services.Container.Get<IFactory>();

        _hud = _factory.Create<HUD>(AssetPathes.UI.HUD);
        HpHandler.Init(_hud.HpBar);
       // WeaponComponent.Init(_factory);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Move(data);
            SerForwardDirection(data);
        }
    }

    private void Move(NetworkInputData data)
    {
        data.Direction.Normalize();
        rb.AddForce(0.5f * data.Direction, ForceMode2D.Impulse);

        if (rb.velocity.magnitude > movementSpeed)
        {
            rb.velocity = rb.velocity.normalized * movementSpeed;
        }

        if (data.Direction == Vector3.zero)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void SerForwardDirection(NetworkInputData networkInputData)
    {
        if (Mathf.Abs(networkInputData.Direction.x) < eps && Mathf.Abs(networkInputData.Direction.x) < eps)
        {
            return;
        }

        _forward = networkInputData.Direction;
    }
}