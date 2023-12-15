using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemy;
    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject door;
    [SerializeField] public GameObject[] doorsReferences;
    public GameObject bossArbolina_GO;
    public GameManager gameManager;
    //Identification of the room
    public int RoomID;
    public bool BossRoom = false;


    //Items
    public List<Items> itemsBuyed;
    public GameObject itemCollectable;

    //Enemies informmation
    [SerializeField] private Enemy[] enemies;
    public bool BossFightActive = false;
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
        else if(RoomID == gameManager.referenceOfRoomGenerator.Length-1)
        {
            y = 8;
            this.gameObject.SetActive(false);
        }
        else
        {
            y = Random.Range(16, 30);
            this.gameObject.SetActive(false);
        }
        matrizTiles = new int[x, y];
        tiles = new GameObject[x, y];
        enemies = new Enemy[10];
        GenerateTerrain();
        GenerateTales();
        GenerateEnemy();
        GenerateDoors();
        if(RoomID != gameManager.referenceOfRoomGenerator.Length - 1)
        {
            GenerateItems();
        }
    }
    public void GenerateItems()
    {
        int r = Random.Range(5, 8);
        for(int i = 0; i <= r; i++)
        {
            if(RoomID != gameManager.referenceOfRoomGenerator.Length - 1)
            {
                while (true)
                {
                    int x = Random.Range(0, 8);
                    int y = Random.Range(5, tiles.GetLength(1));
                    if(tiles[x,y].GetComponent<Tale>().ID == 0 && tiles[x, y] != null)
                    {
                        print("TileID = " + tiles[x, y].GetComponent<Tale>().ID);
                        GameObject instatiateItem = Instantiate(itemCollectable, new Vector2(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y + 0.2f), Quaternion.identity);
                        instatiateItem.GetComponent<ItemCollectable>().item = itemsBuyed[Random.Range(0, itemsBuyed.Count -1)];
                        instatiateItem.transform.parent = transform;
                        instatiateItem.GetComponent<ItemCollectable>().gameManager = this.gameManager;
                        print("IntanciarItem");
                        break;
                    }
                }
            }
        }
        
    }
    private void GenerateDoors()
    {
        print("Cantidad de Rooms: " + gameManager.referenceOfRoomGenerator.Length);
        if (RoomID == 0)
        {
            int x = Random.Range(0, 8);
            int y = Random.Range(5, tiles.GetLength(1));
            doorsReferences = new GameObject[gameManager.referenceOfRoomGenerator.Length];
            int s = 5;
            for(int i = 1; i < gameManager.referenceOfRoomGenerator.Length; i++)
            {
                while (true)
                {
                    x = Random.Range(0, 8);
                    y = Random.Range(s, tiles.GetLength(1));
                    if (tiles[x, y].GetComponent<Tale>().ID != 1)
                    {
                        GameObject instantiatedDoor = Instantiate(door, new Vector2(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y), Quaternion.identity);
                        doorsReferences[i] = instantiatedDoor;
                        instantiatedDoor.GetComponent<Door>().DoorID = i;
                        instantiatedDoor.GetComponent<Door>().posX = x;
                        instantiatedDoor.GetComponent<Door>().posY = y;
                        instantiatedDoor.GetComponent<Door>().gameManager = gameManager;
                        instantiatedDoor.transform.parent = transform;
                        tiles[x, y].GetComponent<Tale>().ID = 3;
                        break;
                    }
                    s += 3;
                }
            }

            
        }
        else if(RoomID != 0 && RoomID != gameManager.referenceOfRoomGenerator.Length - 1)
        {
            int x = Random.Range(0, 8);
            int y = tiles.GetLength(1) - 1;
            GameObject instantiatedDoor = Instantiate(door, new Vector2(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y - 0.2f), Quaternion.identity);
            instantiatedDoor.GetComponent<Door>().DoorID = 0;
            instantiatedDoor.GetComponent<Door>().posX = x;
            instantiatedDoor.GetComponent<Door>().posY = y;
            instantiatedDoor.GetComponent<Door>().gameManager = gameManager;
            instantiatedDoor.transform.parent = transform;
            
        }
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

        if(BossRoom == true)
        {
            for(int i = 0; i < 8; i++)
            {
                matrizTiles[i, matrizTiles.GetLength(1)-1] = 1;
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
                spacex += 0.7f;
                tiles[c,f] = instantiatedTile;
            }
            spacex = 0;
            spacey += 0.05f;
        }

        
    }

    public void GenerateEnemy()
    {
        if (RoomID != gameManager.referenceOfRoomGenerator.Length - 1)
        {
            int s = 10;
            for(int i = 0; i<10; i++)
            {
                while (true)
                {
                    int x = Random.Range(0, 8);
                    int y = Random.Range(5, tiles.GetLength(1));
                    if (tiles[x,y] != null && tiles[x, y].GetComponent<Tale>().ID != 1)
                    {
                        GameObject instantiatedEnemy = Instantiate(Enemy[Random.Range(0,2)], tiles[x,y].transform.position, Quaternion.identity);
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
        else
        {
            GameObject instantiatedBoss = Instantiate(bossArbolina_GO, tiles[4, 7].transform.position, Quaternion.identity);
            instantiatedBoss.transform.position = tiles[4, tiles.GetLength(1)-1].transform.position;
            instantiatedBoss.GetComponent<ArbolinaBoss>().roomGenerator = this.GetComponent<RoomGenerator>();
            instantiatedBoss.GetComponent<ArbolinaBoss>().gameManager = this.gameManager;
            instantiatedBoss.transform.parent = transform;
            bossArbolina_GO = instantiatedBoss;
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
        if(BossFightActive == false)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {

                    enemies[i].EnemyTurn();
                }
            }
        }
        else
        {
            bossArbolina_GO.GetComponent<ArbolinaBoss>().BossAttack();
        }
        
    }

   

    public void PosiblePlayerMovement(int posX, int posY, int quantityMovement)
    {
        //X movement
        int n = 0;
        while (n <= quantityMovement)
        {
            if (posX + n < 8 && tiles[posX + n, posY].GetComponent<Tale>().ID != 1)
            {
                tiles[posX + n, posY].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }

        n = 0;
        while (n <= quantityMovement)
        {
            if (posX - n >= 0 && tiles[posX - n, posY].GetComponent<Tale>().ID != 1)
            {
                tiles[posX - n, posY].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }

        //Y Movement
        n = 0;
        while (n <= quantityMovement)
        {
            if (posY + n < tiles.GetLength(1) && tiles[posX, posY + n].GetComponent<Tale>().ID != 1)
            {
                tiles[posX, posY + n].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }

        n = 0;
        while (n <= quantityMovement)
        {
            if (posY - n >= 0 && tiles[posX, posY - n].GetComponent<Tale>().ID != 1)
            {
                tiles[posX, posY - n].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }
        //Diagonal Up  Movement
        n = 0;
        while (n <= quantityMovement)
        {
            if (posX + n < 8 && posY + n < tiles.GetLength(1) && tiles[posX + n, posY + n].GetComponent<Tale>().ID != 1)
            {
                tiles[posX + n, posY + n].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }

        n = 0;
        while (n <= quantityMovement)
        {
            if (posX - n >= 0 && posY + n < tiles.GetLength(1) && tiles[posX - n, posY + n].GetComponent<Tale>().ID != 1)
            {
                tiles[posX - n, posY + n].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }
        //Diagonal Down Movement
        n = 0;
        while (n <= quantityMovement)
        {
            if (posX + n < 8 && posY - n >= 0 && tiles[posX + n, posY - n].GetComponent<Tale>().ID !=1)
            {
                tiles[posX + n, posY - n].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }

        n = 0;
        while (n <= quantityMovement)
        {
            if (posX - n >= 0 && posY - n >= 0 && tiles[posX - n, posY - n].GetComponent<Tale>().ID != 1)
            {
                tiles[posX - n, posY - n].GetComponent<Tale>().PlayerMovementActive();
            }
            else
            {
                break;
            }
            n++;
        }

    }
}