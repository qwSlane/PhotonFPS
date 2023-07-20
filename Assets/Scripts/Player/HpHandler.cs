using System;
using Fusion;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class HpHandler : NetworkBehaviour
    {
        [SerializeField] private HPBar _hudHpBar;

        [Networked(OnChanged = nameof(OnHpChanged))]
        public int HP { get; set; }

        private const int InitHp = 3;

        private void Start()
        {
            HP = InitHp;
        }

        static void OnHpChanged(Changed<HpHandler> changed)
        {
          //  Debug.Log("Changed");
        }

        public void OnTakeDamage()
        {
            HP -= 1;
            _hudHpBar.TakeDamage();

            if (HP <=0)
            {
                Debug.Log("dead");
            }
        }

        public void Init(HPBar hudHpBar)
        {
            _hudHpBar = hudHpBar;
        }
    }
}