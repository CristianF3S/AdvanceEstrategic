using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "MovementCard" )]
public class MovementCard : ScriptableObject
{
    public string cardName;
    public Sprite cardSprite;
    public int xMovement;
    public int yMovement;

    
}
