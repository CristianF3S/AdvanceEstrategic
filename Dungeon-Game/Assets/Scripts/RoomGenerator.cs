using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject tile;
    //Identification of the room
    public int RoomID;
    public bool BossRoom = false;

    //Enemies informmation
    [SerializeField] private Enemy[] enemies;
    //Tiles information
    public int[,] matrizTiles;
    public GameObject[,] tiles;
    int x = 8;
    int y;

    // Start is called before the first frame update
    void Start()
    {

        if(RoomID == 0)
        {
            y = Random.Range(25, 40);
        }
        else
        {
            y = Random.Range(16, 30);
            this.gameObject.SetActive(false);
        }
        matrizTiles = new int[x, y];
        tiles = new GameObject[x, y];
        enemies = new Enemy[3];
        GenerateTerrain();
        GenerateTales();
        GenerateEnemy();
    }

    private void GenerateTerrain()
    {

        for (int f = 0; f < matrizTiles.GetLength(0); f++)
        {
            for (int c = 0; c < matrizTiles.GetLength(1); c++)
            {
                int n = Random.Range(0, 10);
                if(n > 8)
                {
                    matrizTiles[f,c] = 1;
                }
                else
                {
                    matrizTiles[f, c] = 0;
                }
            }
        }
    }

    private void GenerateTales()
    {
        float spacex = 0;
        float spacey = 0;
        for (int f = 0; f < matrizTiles.GetLength(1); f++)
        {
            for(int c = 0; c < matrizTiles.GetLength(0); c++)
            {
                GameObject instantiatedTile = Instantiate(tile, new Vector2(c + spacex, f + spacey), Quaternion.identity);
                instantiatedTile.GetComponent<Tale>().ReceiveDates(matrizTiles[c, f], c, f);
                instantiatedTile.transform.parent = transform;
                spacex += 0.1f;
                tiles[c,f] = instantiatedTile;
            }
            spacex = 0;
            spacey += 0.1f;
        }
    }

    public void GenerateEnemy()
    {
        int s = 3;
        for(int i = 0; i<3; i++)
        {
            while (true)
            {
                int x = Random.Range(0, 8);
                int y = Random.Range(s, tiles.GetLength(1) - 5);
                if (tiles[x, y].GetComponent<Tale>().ID != 1)
                {
                    GameObject instantiatedEnemy = Instantiate(Enemy, tiles[x,y].transform.position, Quaternion.identity);
                    instantiatedEnemy.GetComponent<Enemy>().x = tiles[x, y].GetComponent<Tale>().posX;
                    instantiatedEnemy.GetComponent<Enemy>().y = tiles[x, y].GetComponent<Tale>().posY;
                    instantiatedEnemy.GetComponent<Enemy>().roomGenerator = this.GetComponent<RoomGenerator>();
                    instantiatedEnemy.transform.parent = transform;
                    enemies[i] = instantiatedEnemy.GetComponent<Enemy>();
                    break;
                }
            }
            s += 5;
        }
    }

    public void PosiblePlayerMovement(int posX, int posY, int quantityMovement)
    {
        int n = 0;
        while(n <= quantityMovement)
        {
            if(posX + n < 8 && posY + n < tiles.GetLength(1))
            {
                tiles[posX + n, posY].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX, posY + n].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX + n, posY + n].GetComponent<Tale>().PlayerMovementActive();

            }

            if (posX + n < 8 && posY - n >= 0)
            {
                tiles[posX + n, posY].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX, posY - n].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX + n, posY - n].GetComponent<Tale>().PlayerMovementActive();
            }

            if (posX - n >= 0 && posY + n < tiles.GetLength(1))
            {
                tiles[posX - n, posY].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX, posY + n].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX - n, posY + n].GetComponent<Tale>().PlayerMovementActive();
            }

            if (posX - n >= 0 && posY - n >= 0)
            {
                tiles[posX - n, posY].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX, posY - n].GetComponent<Tale>().PlayerMovementActive();
                tiles[posX - n, posY - n].GetComponent<Tale>().PlayerMovementActive();
            }
            n++;
        }
    }

    public void DesactivePosibleToMove()
    {
        for (int f = 0; f < tiles.GetLength(1); f++)
        {
            for (int c = 0; c < tiles.GetLength(0); c++)
            {
                tiles[c, f].GetComponent<Tale>().DesativeCollider();
            }
        }
    }

    //Enemies Activated
    public void EnemiesAttack()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {

                enemies[i].EnemyTurn();
            }
        }
    }
}
