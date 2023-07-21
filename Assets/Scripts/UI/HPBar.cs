using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image[] Hp = new Image[3];

    private int CurrentHp = 3;

    public void TakeDamage()
    {
        CurrentHp -= 1;
        Hp[CurrentHp].color = Color.clear;
    }
}