using System.Linq;
using DefaultNamespace;
using DefaultNamespace.Player;
using DefaultNamespace.Services;
using DefaultNamespace.Services.Factory;
using DefaultNamespace.UI;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : NetworkBehaviour
{
    [SerializeField] private Hitbox _hitbox;
    [SerializeField] private HpHandler _hpHandler;
    [SerializeField] private PickHandler _pickHandler;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public TextMeshProUGUI PlayerNickName;

    private Vector2 _forward;

    [Networked(OnChanged = nameof(OnNickChanged))]
    public NetworkString<_8> NickName { get; set; }

    private float movementSpeed = 110f;
    private const float eps = 0.05f;

    public Hitbox Hitbox => _hitbox;
    public Vector2 Forward => _forward;

    public bool IsAlive()
    {
        return _hpHandler.isDead;
    }

    static void OnNickChanged(Changed<Player> changed)
    {
        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        PlayerNickName.text = NickName.ToString();
    }

    public (string, int) GetPlayerData()
    {
        return (NickName.ToString(), _pickHandler.CoinCount);
    }

    public override void Spawned()
    {
        base.Spawned();

        var runner  = Services.Container.Get<INetworkRunner>();
        var factory = Services.Container.Get<IFactory>();
        if (Object.HasInputAuthority)
        {
            var hud = factory.Create<HUD>(AssetPathes.UI.HUD);
            _hpHandler.Init(hud.HpBar, runner, this);
            _pickHandler.Init(hud.CoinsBar);

            RPC_SetNickName(PlayerPrefs.GetString("NickName"));

            runner.AddPlayer(Runner.LocalPlayer, this);

            int color = Runner.ActivePlayers.Count() % 3;

            switch (color)
            {
                case 2:
                    _spriteRenderer.color = Color.blue;
                    break;
                case 1:
                    _spriteRenderer.color = Color.green;
                    break;
            }
            
            
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Move(data);
            SerForwardDirection(data);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_SetNickName(string nickname)
    {
        this.NickName = nickname;
    }

    private void Move(NetworkInputData data)
    {
        data.Direction.Normalize();
        _rb.AddForce(4 * data.Direction, ForceMode2D.Impulse);

        if (_rb.velocity.magnitude > movementSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * movementSpeed;
        }

        if (data.Direction == Vector3.zero)
        {
            _rb.velocity = Vector3.zero;
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