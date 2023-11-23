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
            print(hit.collider);

            // Comprueba si se ha golpeado un objeto
            if (hit.collider != null)
            {
                print("Rayo");
                // Accede al sprite del objeto golpeado
                SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();

                // Hacer algo con el sprite (por ejemplo, cambiar el color)
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.blue;
                }
            }
            else
            {

                print("Rayo1");
            }
        }
    }
}
