using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbolinaBoss : MonoBehaviour
{
    public RoomGenerator roomGenerator;
    public GameManager gameManager;

    public GameObject Attack_GO;
    public float life = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossAttack()
    {
        if(life > 75)
        {
            AttackPhaseOne();
        }
        else if(life > 30)
        {
            AttackPhaseTwo();
        }
        else
        {
            AttackPhaseThree();
        }
    }

    public void AttackPhaseOne()
    {
        for(int i = 0; i < 3; i++)
        {
            int x = Random.Range(0, 8);
            int y = Random.Range(0, roomGenerator.tiles.GetLength(1));

            Instantiate(Attack_GO, roomGenerator.tiles[x, y].transform.position, Quaternion.identity);
        }
    }

    public void AttackPhaseTwo()
    {

    }

    public void AttackPhaseThree()
    {

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
