using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public int posX = 4;
    public int posY = 5;
    // Start is called before the first frame update
    void Start()
    {
        posX = 4;
        posY = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Lanza un rayo desde la posición del ratón
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Comprueba si se ha golpeado un objeto
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Tiles")
                {
                    posX = hit.collider.GetComponent<Tale>().posX;
                    posY = hit.collider.GetComponent<Tale>().posY;
                    this.transform.position = hit.collider.transform.position;
                    gameManager.PlayerPlayed();
                }
            }

        }
    }

}
