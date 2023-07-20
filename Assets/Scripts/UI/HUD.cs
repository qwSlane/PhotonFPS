using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private HPBar _hpBar;

        public HPBar HpBar => _hpBar;
    }
}