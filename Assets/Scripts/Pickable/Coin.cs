using DefaultNamespace.Player;
using Fusion;
using UnityEngine;

namespace DefaultNamespace.Pickable
{
    public class Coin : NetworkBehaviour, ICollidable
    {
        [Networked(OnChanged = nameof(OnIsEnabledChangedCallback))]
        public NetworkBool IsActive { get; set; } = true;

        public Transform visuals;

        public bool Collide(PickHandler handler)
        {
            if (IsActive)
            {
                handler.Increase();

                IsActive = false;

                if (handler.Object.HasStateAuthority)
                {
                    Runner.Despawn(Object);
                }
            }

            return true;
        }

        private static void OnIsEnabledChangedCallback(Changed<Coin> changed)
        {
            var behaviour = changed.Behaviour;
            behaviour.visuals.gameObject.SetActive(behaviour.IsActive);
        }
    }
}