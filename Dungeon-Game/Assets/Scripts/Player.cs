using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask collisionLayer;
    public GameManager gameManager;
    public int posX = 4;
    public int posY = 5;

    //Habilities
    public float life;
    public float powerAttack;
    public int quantityMovement;
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
                    PlayerAttack();

                }
            }

        }
    }

    public void PlayerAttack()
    {
        CastRay(Vector2.up);
        CastRay(Vector2.up + Vector2.right);
        CastRay(Vector2.right);
        CastRay(Vector2.right - Vector2.up);
        CastRay(-Vector2.up);
        CastRay(-Vector2.up - Vector2.right);
        CastRay(-Vector2.right);
        CastRay(-Vector2.right + Vector2.up);
        gameManager.PlayerTurnFinished();
    }

    void CastRay(Vector2 direction)
    {
        float maxRayDistance = 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, collisionLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Enemy>().ReceiveDamage(powerAttack);
            }
        }
    }

    public void ReceiveDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
