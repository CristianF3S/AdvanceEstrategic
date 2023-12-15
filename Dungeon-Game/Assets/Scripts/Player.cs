using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public LayerMask collisionLayer;
    public GameManager gameManager;
    public int posX = 4;
    public int posY = 5;

    //Weapon
    public Weapon weapon;
    public string nameAttack;
    public float[] powerWeaponAttack;
    public Vector2[] attackPosition;
    public Sprite[] spritesWeapon;

    //Habilities
    public float life;
    public float powerAttack;
    public int quantityMovement;
    // Start is called before the first frame update
    void Start()
    {
        name = weapon.nameAttack;
        powerWeaponAttack = weapon.powerAttack;
        attackPosition = weapon.attackPosition;
        spritesWeapon = weapon.spritesWeapon;
        posX = 4;
        posY = 1;
        transform.position = gameManager.referenceOfRoomGenerator[gameManager.idRoomActive].GetComponent<RoomGenerator>().tiles[posX, posY].transform.position;
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.4f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            ReceiveDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            life += 10;
        }

        gameManager.LifePlayerManagment(life);

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
                    this.transform.position = new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y+0.4f);
                    gameManager.PlayerPlayed();
                    PlayerAttack1();

                }
            }

        }
    }


    public void PlayerAttack1()
    {
        for(int i = 0; i < attackPosition.Length; i++)
        {
            if (posX + (int)attackPosition[i].x >= 0 && posX + (int)attackPosition[i].x < 8 && posY + (int)attackPosition[i].y < gameManager.referenceOfRoomGenerator[gameManager.idRoomActive].GetComponent<RoomGenerator>().tiles.GetLength(1))
            {
                GameObject projectile = Instantiate(weapon.projectile, new Vector2(posX + attackPosition[i].x, posY + attackPosition[i].y), Quaternion.identity);
                projectile.GetComponent<Projectile>().posX = posX + (int)attackPosition[i].x;
                projectile.GetComponent<Projectile>().posY = posY + (int)attackPosition[i].y;
                projectile.GetComponent<Projectile>().gameManager = gameManager;
                projectile.GetComponent<Projectile>().powerAttack = powerAttack;
                gameManager.PlayerTurnFinished();
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
