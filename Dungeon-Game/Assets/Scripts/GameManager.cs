using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject roomGeneratorObj;
    [SerializeField] GameObject[] referenceOfRoomGenerator;
    [SerializeField] GameObject player;
    [SerializeField] MovementCard_SO[] movementCard_SO;
    [SerializeField] GameObject[] movementCards_GO;
    //Gameplay
    bool playerSelected = false;

    //Quantity of worlds
    int numberOfRooms;

    // Start is called before the first frame update
    void Start()
    {
        numberOfRooms = Random.Range(4, 7);
        referenceOfRoomGenerator = new GameObject[numberOfRooms];
        GenerateRooms();
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
    }

    public void PlayerTurn()
    {
        //Activar cartas
        for(int i = 0; i < movementCards_GO.Length; i++)
        {
            movementCards_GO[i].SetActive(true);
        }
    }


}
