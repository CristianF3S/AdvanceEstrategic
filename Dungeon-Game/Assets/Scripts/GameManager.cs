using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject roomGeneratorObj;
    [SerializeField] GameObject[] referenceOfRoomGenerator;
    [SerializeField] int idRoomActive;
    //Reference to the player
    [SerializeField] GameObject player;
    Player playerScript;
    public int QuantityPlayerMovement = 1;


    [SerializeField] MovementCard_SO[] movementCard_SO; //Tener los 3 Card Movement seteados
    [SerializeField] GameObject[] movementCards_GO; // Tener los 3 profabs que contienen los Scritable Object
    //Gameplay
    bool playerSelected = false;

    //Quantity of worlds
    int numberOfRooms;

    // Start is called before the first frame update
    void Start()
    {
        numberOfRooms = Random.Range(4, 7);
        referenceOfRoomGenerator = new GameObject[numberOfRooms];
        idRoomActive = 0;
        GenerateRooms();
        StartCoroutine(pruebas());
    }

    private void GenerateRooms()
    {
        for (int c = 0; c < numberOfRooms; c++)
        {
            GameObject instantiatedTile = Instantiate(roomGeneratorObj, new Vector2(0,0), Quaternion.identity);
            referenceOfRoomGenerator[c] = instantiatedTile;
            instantiatedTile.transform.parent = transform;
            instantiatedTile.GetComponent<RoomGenerator>().RoomID = c;

            if(c == numberOfRooms - 1)
            {
                instantiatedTile.GetComponent<RoomGenerator>().BossRoom = true;
            }
        }

        player = Instantiate(player, new Vector3(4.4f, 0, -1), Quaternion.identity);
        playerScript = player.GetComponent<Player>();
        playerScript.gameManager = this.gameObject.GetComponent<GameManager>();
    }

    public void PlayerTurn()
    {
        referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().PosiblePlayerMovement(playerScript.posX, playerScript.posY, QuantityPlayerMovement);
    }

    public void PlayerPlayed()
    {
        referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().DesactivePosibleToMove();

        StartCoroutine(pruebas());
    }

    IEnumerator pruebas()
    {
        yield return new WaitForSeconds(2);
        PlayerTurn();
    }

}
