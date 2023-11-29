using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tale : MonoBehaviour
{
    Collider2D collider2D;

    //Tile dentification
    public int ID;
    public int posX, posY;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
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
        if(ID == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            collider2D.enabled = false;
        }
    }

    public void PlayerMovementActive()
    {
        if(ID != 1)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            collider2D.enabled = true;
        }
        
    }

    public void DesativeCollider()
    {
        if (ID != 1)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            collider2D.enabled = false;

        }
    }
}
