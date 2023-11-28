using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                    this.transform.position = hit.collider.transform.position;
                    this.GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }

        }
    }

    
}
