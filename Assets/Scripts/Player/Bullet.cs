using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Bullet : NetworkBehaviour
    {
        private Vector2 _direction;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private NetworkObject _networkObject;
        private Hitbox _parent;

        public void Init(Vector2 direction, Hitbox parent)
        {
            _direction    = direction;
            _parent       = parent;
        }

        private bool IsHit()
        {
            List<LagCompensatedHit> hits = new List<LagCompensatedHit>();
            Runner.LagCompensation.OverlapSphere(transform.localPosition, 0.5f, Object.InputAuthority, hits, layerMask,
                HitOptions.IncludePhysX);

            if (hits.Count != 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.Hitbox == _parent)
                    {
                        continue;
                    }

                    hit.Hitbox.transform.root.TryGetComponent<HpHandler>(out var component);
                    if (component != null)
                    {
                        //   component.OnTakeDamage();
                    }
                    return true;
                }
            }

            return false;
        }

        public override void FixedUpdateNetwork()
        {
            transform.Translate(_direction * 20f * Runner.DeltaTime);
                if (IsHit())
                    Runner.Despawn(Object);
            
        }
    }
}