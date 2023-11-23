using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    //Identification of the room
    public int RoomID;
    public bool BossRoom = false;

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
        GenerateTerrain();
        GenerateTales();
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
                instantiatedTile.transform.parent = transform;
                spacex += 0.1f;
                tiles[c,f] = instantiatedTile;

                if (matrizTiles[c,f] == 1)
                {
                    instantiatedTile.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            spacex = 0;
            spacey += 0.1f;
        }
    }
}
