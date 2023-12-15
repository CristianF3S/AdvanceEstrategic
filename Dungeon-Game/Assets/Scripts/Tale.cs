using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tale : MonoBehaviour
{
    Collider2D collider2D;
    public Sprite[] sprites;
    public Color transparente, white;
    public GameObject MovementTile;

    //Tile dentification
    public int ID;
    public int posX, posY;
    public SpriteRenderer sprite, sprite2;

    // Start is called before the first frame update
    void Start()
    {
        sprite.sortingOrder -= posY;
        sprite2.sortingOrder -= posY;
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        MovementTile.SetActive(false);
        TilesAparience();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDates(int ID, int posX, int posY)
    {
        this.ID = ID;
        this.posX = posX;
        this.posY = posY;
    }

    public void TilesAparience()
    {
        if(posY < 3)
        {
            ID = 0;
        }
        if(ID == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = transparente;
            sprite.sprite = sprites[Random.Range(0, 3)];
            collider2D.enabled = false;
        }
        else if(ID == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = transparente;
            sprite.sprite = sprites[Random.Range(3, 8)];
        }

    }

    public void PlayerMovementActive()
    {
        if(ID != 1)
        {
            //GetComponent<SpriteRenderer>().color = white;
            MovementTile.SetActive(true);
            collider2D.enabled = true;
        }
        
    }

    public void DesativeCollider()
    {
        if (ID != 1)
        {
            //GetComponent<SpriteRenderer>().color = transparente;

            MovementTile.SetActive(false);
            collider2D.enabled = false;

        }
    }
}
