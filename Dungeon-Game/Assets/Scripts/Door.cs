using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LayerMask collisionLayer;
    public int DoorID;
    public int posX, posY;

    bool doorClosed = true;

    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        doorClosed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorClosed)
        {
            CastRay(Vector2.up);
        }
    }
    public void CheckPlayer()
    {
        print("Check 2");
    }

    void CastRay(Vector2 direction)
    {
        float maxRayDistance = 0.3f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, collisionLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                doorClosed = false;
                gameManager.ChangeTheRoom(DoorID);
            }
        }
    }

    
}
