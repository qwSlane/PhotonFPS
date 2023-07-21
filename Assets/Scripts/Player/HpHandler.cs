using DefaultNamespace.Services;
using Fusion;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class HpHandler : NetworkBehaviour
    {
        [SerializeField] private HPBar _hudHpBar;

        private INetworkRunner _runner;
        private global::Player _root;

        [Networked(OnChanged = nameof(OnHpChanged))]
        public int HP { get; set; }

        [Networked(OnChanged = nameof(OnDead))]
        public bool isDead { get; set; }

        private const int InitHp = 3;

        public void Init(HPBar hudHpBar, INetworkRunner runner, global::Player player)
        {
            _root     = player;
            _runner   = runner;
            _hudHpBar = hudHpBar;
        }

        private void Start()
        {
            HP     = InitHp;
            isDead = false;
        }

        static void OnDead(Changed<HpHandler> changed)
        {
            var player = changed.Behaviour._runner.GetAlive().GetPlayerData();
            changed.Behaviour.RPC_End(player.Item1, player.Item2.ToString());
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        private void RPC_End(string nick, string coins)
        {
            Instantiate(Resources.Load<Winner>(AssetPathes.UI.Winner))
                .Init(nick, coins);
        }

        static void OnHpChanged(Changed<HpHandler> changed)
        {
            if (changed.Behaviour.isDead)
            {
                return;
            }

            changed.Behaviour.OnHPReduced();
        }

        private void OnHPReduced()
        {
            if (HP == InitHp)
            {
                return;
            }

            _hudHpBar.TakeDamage();

            if (HP <= 0)
            {
                isDead = true;
              //  Debug.Log("Dead");
            }
        }

        public void OnTakeDamage()
        {
            HP -= 1;
        }
    }
}