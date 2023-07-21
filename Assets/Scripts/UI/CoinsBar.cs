using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class CoinsBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsCount;

        public void UpdateCoins(int current)
        {
            _coinsCount.text = current.ToString();
        }
    }
}