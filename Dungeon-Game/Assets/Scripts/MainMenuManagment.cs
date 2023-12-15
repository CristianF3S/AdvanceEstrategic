using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManagment : MonoBehaviour
{
    public ItemsData[] itemsData; 
    public GameObject dataObject;
    public PlayerData playerData;
    public TextMeshProUGUI moneyText;

    public int Dinero;
    // Start is called before the first frame update
    void Start()
    {
        dataObject = GameObject.Find("Data");
        playerData = dataObject.GetComponent<PlayerData>();
        Dinero = playerData.dinero;
        SearchingItems();

    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = playerData.dinero.ToString();
    }

    public void SearchingItems()
    {
        for(int i = 0; i < itemsData.Length; i++)
        {

            foreach (int ID in playerData.items)
            {
                if(ID == itemsData[i].ID)
                {
                    itemsData[i].ItemWasBuyed();
                }
            }
        }
    }
}
