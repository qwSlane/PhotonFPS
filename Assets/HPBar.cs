using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image[] Hp = new Image[3];

    private int CurrentHp = 3;

    public void TakeDamage()
    {
        Debug.Log("taken dmg");
        CurrentHp -= 1;
        Hp[CurrentHp].color = Color.clear;
    }
}