using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Bullet : NetworkBehaviour
    {
        private Vector2 _direction;
        [SerializeField] private LayerMask layerMask;
        private Hitbox _parent;
        private bool isInitialized = false;

        public void Init(Vector2 direction, Hitbox parent)
        {
            _direction    = direction;
            _parent       = parent;
            isInitialized = true;
        }

        private bool IsHit()
        {
            List<LagCompensatedHit> hits = new List<LagCompensatedHit>();
            Runner.LagCompensation.OverlapSphere(transform.localPosition, 5f, Object.InputAuthority, hits, layerMask,
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
                        component.OnTakeDamage();
                    }
                    return true;
                }
            }

            return false;
        }

        public override void FixedUpdateNetwork()
        {
            if (!isInitialized)
                return;
            transform.Translate(_direction * 400f * Runner.DeltaTime);
            if (IsHit())
                Runner.Despawn(Object);
        }
    }
}