using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectable : MonoBehaviour
{
    public LayerMask collisionLayer;
    public Items item;

    public GameObject marco;
    public GameObject itemSprite;

    public int Movimiento;
    public float life;
    public float powerAttack;

    bool collected;
    // Start is called before the first frame update
    void Start()
    {
        marco.GetComponent<SpriteRenderer>().sprite = item.marcos[item.nivel];
        itemSprite.GetComponent<SpriteRenderer>().sprite = item.sprite;

        Movimiento = item.Movimiento;
        life = item.life;
        powerAttack = item.powerAttack;
        collected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(collected)
        {
            CastRay(Vector2.up);
        }
    }

    void CastRay(Vector2 direction)
    {
        float maxRayDistance = 0.3f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, collisionLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                collected = false;
                hit.collider.gameObject.GetComponent<Player>().quantityMovement += Movimiento;
                hit.collider.gameObject.GetComponent<Player>().powerAttack += powerAttack;
                hit.collider.gameObject.GetComponent<Player>().life += life;
                Destroy(this.gameObject);
            }
        }
    }

}
