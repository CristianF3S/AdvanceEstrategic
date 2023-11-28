using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "MovementCard" )]
public class MovementCard_SO : ScriptableObject
{
    public MovementCard_SO movementCard;

    public string cardName;
    public Sprite cardSprite;
    public Vector2Int pos1Movement;
    public Vector2Int pos2Movement;

    
}
