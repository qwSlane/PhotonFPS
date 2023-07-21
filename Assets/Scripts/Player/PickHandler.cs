using DefaultNamespace.Pickable;
using DefaultNamespace.UI;
using Fusion;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class PickHandler : NetworkBehaviour
    {
        private CoinsBar _hudCoinsBar;

        [Networked(OnChanged = nameof(OnCoinCountChange))]
        public int CoinCount { get; set; } = 0;

        public void Init(CoinsBar hudCoinsBar)
        {
            _hudCoinsBar = hudCoinsBar;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICollidable collidable))
            {
                collidable.Collide(this);
            }
        }

        private static void OnCoinCountChange(Changed<PickHandler> changed)
        {
            changed.Behaviour._hudCoinsBar.UpdateCoins(changed.Behaviour.CoinCount);
        }

        public void Increase()
        {
            CoinCount++;
        }
    }
}