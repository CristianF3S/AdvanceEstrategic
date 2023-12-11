using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string nameAttack;
    public float[] powerAttack;
    public Vector2[] attackPosition;
    public Sprite[] spritesWeapon;
    public GameObject projectile;
}
