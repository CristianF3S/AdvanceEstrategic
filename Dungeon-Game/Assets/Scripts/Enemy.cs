using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject dataGame;
    public PlayerData dataPlayer;

    public LayerMask collisionLayer;
    public int x, y;
    public float life;

    //Habilities
    public float powerAttack;

    [SerializeField] public RoomGenerator roomGenerator;
    // Start is called before the first frame update
    void Start()
    {
        dataGame = GameObject.Find("Data");
        dataPlayer = dataGame.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyTurn()
    {
        bool find = true;
        int xMove = x;
        int yMove = y;
        while (find)
        {
            int n = Random.Range(1, 10);
            switch (n)
            {
                case 1:
                    if (y + 1 < roomGenerator.tiles.GetLength(1))
                    {
                        xMove = x;
                        yMove = y+1;
                        find = false;
                    }
                    break;
                case 2:
                    if (x+1 < 8 && y+1 < roomGenerator.tiles.GetLength(1))
                    {
                        xMove = x+1;
                        yMove = y+1;
                        find = false;
                    }
                    break;
                case 3:
                    if (x - 1 >= 0 && y + 1 < roomGenerator.tiles.GetLength(1))
                    {
                        xMove = x - 1;
                        yMove = y + 1;
                        find = false;
                    }
                    break;
                case 4:
                    if (x + 1 < 8 && y < roomGenerator.tiles.GetLength(1))
                    {
                        xMove = x + 1;
                        yMove = y;
                        find = false;
                    }
                    break;
                case 5:
                    if (x - 1 >= 0 && y < roomGenerator.tiles.GetLength(1))
                    {
                        xMove = x - 1;
                        yMove = y;
                        find = false;
                    }
                    break;
                
                case 6:
                    if (x + 1 < 8 && y - 1 >= 0)
                    {
                        xMove = x + 1;
                        yMove = y - 1;
                        find = false;
                    }
                    break;
                case 7:
                    if (x - 1 >= 0 && y - 1 >= 0)
                    {
                        xMove = x - 1;
                        yMove = y - 1;
                        find = false;
                    }
                    break;
                default:
                    if (x < 8 && y - 1 >= 0)
                    {
                        xMove = x;
                        yMove = y - 1;
                        find = false;
                    }
                    break;
            }
            if(roomGenerator.tiles[xMove, yMove].GetComponent<Tale>().ID == 1 || roomGenerator.tiles[xMove, yMove].GetComponent<Tale>().ID == 3)
            {
                find = true;
            }
        }
        x = xMove;
        y = yMove;
        this.transform.position = roomGenerator.tiles[xMove, yMove].transform.position;
        EnemyAttackPlayer();
    }

    public void EnemyAttackPlayer()
    {
        CastRay(Vector2.up);
        CastRay(Vector2.up + Vector2.right);
        CastRay(Vector2.right);
        CastRay(Vector2.right - Vector2.up);
        CastRay(-Vector2.up);
        CastRay(-Vector2.up - Vector2.right);
        CastRay(-Vector2.right);
        CastRay(-Vector2.right + Vector2.up);
    }

    void CastRay(Vector2 direction)
    {
        float maxRayDistance = 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRayDistance, collisionLayer);

        if (hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Player>().ReceiveDamage(powerAttack);
            }
        }
    }

    public void ReceiveDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            dataPlayer.dinero = Random.Range(1, 4);
            Destroy(this.gameObject);
        }
    }

    
}
