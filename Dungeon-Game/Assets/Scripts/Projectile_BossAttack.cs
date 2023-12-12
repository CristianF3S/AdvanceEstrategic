using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_BossAttack : MonoBehaviour
{
    public LayerMask collisionLayer;
    // Start is called before the first frame update
    void Start()
    {
        BossAttack();
        StartCoroutine(Time());

    }
    public void BossAttack()
    {
        CastRay(Vector2.up);
    }

    void CastRay(Vector2 direction)
    {
        float maxRayDistance = 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, collisionLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                print("Ataco");
                hit.collider.GetComponent<Player>().ReceiveDamage(100);
            }
        }
    }
    IEnumerator Time()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
