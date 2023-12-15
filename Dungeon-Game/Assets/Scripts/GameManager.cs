using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject roomGeneratorObj;
    public GameObject[] referenceOfRoomGenerator;
    public int idRoomActive;
    //Reference to the player
    public GameObject player;
    public Player playerScript;
    //weapon
    [SerializeField] Weapon[] weapons;
    public int weaponSelected;
    public int[] weaponLevel;
    bool weaponActive;

    //Items
    public Items[] items;
    public List<Items> itemsBuyed;
    public GameObject imageItemUI;
    public GameObject parentimageItemUI;

    public GameObject dataGame;
    public PlayerData dataPlayer;

    //UI
    public TextMeshProUGUI moneyText, stregthText;
    bool inGame;


    [SerializeField] MovementCard_SO[] movementCard_SO; //Tener los 3 Card Movement seteados
    [SerializeField] GameObject[] movementCards_GO; // Tener los 3 profabs que contienen los Scritable Object
    //Gameplay
    bool playerSelected = false;

    //Quantity of worlds
    int numberOfRooms;

    // Start is called before the first frame update
    void Start()
    {
        inGame = false;
        dataGame = GameObject.Find("Data");
        dataPlayer = dataGame.GetComponent<PlayerData>();
        weaponActive = true;
        //Items in game
        for (int i = 0; i < items.Length; i++)
        {
            foreach(int ID in dataPlayer.items)
            {
                if (items[i].ID == ID)
                {
                    itemsBuyed.Add(items[i]);
                }
            }
        }

        numberOfRooms = Random.Range(4, 7);
        referenceOfRoomGenerator = new GameObject[numberOfRooms];
        idRoomActive = 0;
        GenerateRooms();
        
        
    }
    private void Update()
    {
        if (inGame)
        {

            moneyText.text = dataPlayer.dinero.ToString();
            stregthText.text = playerScript.powerAttack.ToString();
        }
    }

    private void GenerateRooms()
    {
        for (int c = 0; c < numberOfRooms; c++)
        {
            GameObject instantiatedTile = Instantiate(roomGeneratorObj, new Vector2(0,0), Quaternion.identity);
            referenceOfRoomGenerator[c] = instantiatedTile;
            instantiatedTile.transform.parent = transform;
            instantiatedTile.GetComponent<RoomGenerator>().RoomID = c;
            instantiatedTile.GetComponent<RoomGenerator>().itemsBuyed = this.itemsBuyed;

            instantiatedTile.GetComponent<RoomGenerator>().gameManager = this.GetComponent<GameManager>();

            if (c == numberOfRooms - 1)
            {
                instantiatedTile.GetComponent<RoomGenerator>().BossRoom = true;
                instantiatedTile.GetComponent<RoomGenerator>().BossFightActive = true;
            }
        }

        
    }

    public void PlayerTurn()
    {
        referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().PosiblePlayerMovement(playerScript.posX, playerScript.posY, playerScript.quantityMovement);
    }

    //Player
    public void PlayerPlayed()
    {
        referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().DesactivePosibleToMove();
        if(referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().BossFightActive != true)
        {
            MoveTheCamera();
        }
    }

    public void PlayerTurnFinished()
    {
        StartCoroutine(pruebas());
    }

    //Management
    IEnumerator pruebas()
    {
        yield return new WaitForSeconds(2);
        referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().EnemiesAttack();
        yield return new WaitForSeconds(2);
        PlayerTurn();
    }


    public void MoveTheCamera()
    {
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 1.1f, camera.transform.position.z);
    }

    public void ChangeTheRoom(int DoorID)
    {
        if(DoorID!=0 && DoorID != referenceOfRoomGenerator.Length - 1)
        {
            referenceOfRoomGenerator[idRoomActive].SetActive(false);
            idRoomActive = DoorID;
            referenceOfRoomGenerator[idRoomActive].SetActive(true);

            camera.transform.position = new Vector3(4.5f, 4.5f, -10);

            player.transform.position = new Vector3(4.4f, 1.1f, 0);
            playerScript.posX = 4;
            playerScript.posY = 1;

            player.transform.position = new Vector2(referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().tiles[playerScript.posX, playerScript.posY].transform.position.x, referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().tiles[playerScript.posX, playerScript.posY].transform.position.y + 0.4f);

        }
        else if(DoorID == referenceOfRoomGenerator.Length - 1)
        {
            referenceOfRoomGenerator[idRoomActive].SetActive(false);
            idRoomActive = DoorID;
            referenceOfRoomGenerator[idRoomActive].SetActive(true);

            camera.transform.position = new Vector3(4.5f, 4.5f, -10);

            
            playerScript.posX = 4;
            playerScript.posY = 1;
            player.transform.position = new Vector2(referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().tiles[playerScript.posX, playerScript.posY].transform.position.x, referenceOfRoomGenerator[idRoomActive].GetComponent<RoomGenerator>().tiles[playerScript.posX, playerScript.posY].transform.position.y + 0.4f);

            //Activar Boss
        }
        else if(DoorID == 0)
        {
            referenceOfRoomGenerator[idRoomActive].SetActive(false);
            Vector3 playerPos = referenceOfRoomGenerator[DoorID].GetComponent<RoomGenerator>().doorsReferences[idRoomActive].transform.position;
            player.transform.position = new Vector2 (playerPos.x, playerPos.y+0.2f);
            playerScript.posX = referenceOfRoomGenerator[DoorID].GetComponent<RoomGenerator>().doorsReferences[idRoomActive].GetComponent<Door>().posX;
            playerScript.posY = referenceOfRoomGenerator[DoorID].GetComponent<RoomGenerator>().doorsReferences[idRoomActive].GetComponent<Door>().posY;

            camera.transform.position = new Vector3(camera.transform.position.x, playerPos.y+0.2f, camera.transform.position.z);

            idRoomActive = DoorID;
            referenceOfRoomGenerator[idRoomActive].SetActive(true);


        }


    }

    public void StarGame(int num)
    {
        weaponSelected = num;
        player = Instantiate(player, new Vector3(4.4f, 1.1f, 0), Quaternion.identity);
        playerScript = player.GetComponent<Player>();
        playerScript.gameManager = this.gameObject.GetComponent<GameManager>();
        playerScript.weapon = weapons[weaponSelected];
        inGame = true;
        StartCoroutine(pruebas());
    }

    public void InstanciarItemEnUI(Sprite sprite)
    {
        print("ITEM");
        GameObject instantiatedItem = Instantiate(imageItemUI, new Vector2(0,0), Quaternion.identity);
        instantiatedItem.GetComponent<Image>().sprite = sprite;
        instantiatedItem.transform.SetParent(parentimageItemUI.transform);
    }

}
