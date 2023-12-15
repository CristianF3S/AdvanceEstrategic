using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public LayerMask collisionLayer;
    public int posX;
    public int posY;
    Vector2 position;
    public float powerAttack;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = gameManager.referenceOfRoomGenerator[gameManager.idRoomActive].GetComponent<RoomGenerator>().tiles[posX, posY].transform.position;
        PlayerAttack();
        StartCoroutine(Time());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerAttack()
    {
        CastRay(Vector2.up);
    }

    void CastRay(Vector2 direction)
    {
        float maxRayDistance = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, collisionLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy")
            {
                print("Ataco");
                hit.collider.GetComponent<Enemy>().ReceiveDamage(powerAttack);
            }

            if (hit.collider.tag == "Boss")
            {
                print("Ataco");
                hit.collider.GetComponent<ArbolinaBoss>().ReceiveDamage(powerAttack);
            }
        }
    }



    IEnumerator Time()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
