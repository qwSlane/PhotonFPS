using TMPro;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Winner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nick;
        [SerializeField] private TextMeshProUGUI _coins;
        
        public void Init(string nick, string coins)
        {
            _nick.text  = nick;
            _coins.text = coins;
        }
    }
}