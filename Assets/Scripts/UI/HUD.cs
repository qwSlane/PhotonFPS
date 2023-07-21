using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private HPBar _hpBar;
        [SerializeField] private CoinsBar _coinsBar;

        public CoinsBar CoinsBar => _coinsBar;

        public HPBar HpBar => _hpBar;
    }
}