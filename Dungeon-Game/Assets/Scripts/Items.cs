using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Item Information")]
public class Items : ScriptableObject
{
    public string name;
    public int ID;
    public int cost;
    public int nivel;
    public Sprite sprite;
    public Sprite[] marcos;

    public bool buyed;
    public GameObject spriteItem;

    public int Movimiento;
    public float life;
    public float powerAttack;
}
