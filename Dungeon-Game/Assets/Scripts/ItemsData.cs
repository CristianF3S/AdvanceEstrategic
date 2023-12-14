using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsData : MonoBehaviour
{
    public GameObject dataObject;
    public PlayerData playerData;
    public GameObject itembuyedCorrectly;
    public Items item;
    public string name;
    public int ID;
    public int cost;
    public bool buyed;

    public int Movimiento;
    public float life;
    public float powerAttack;
    // Start is called before the first frame update
    void Start()
    {
        dataObject = GameObject.Find("Data");
        playerData = dataObject.GetComponent<PlayerData>();
        name = item.name;
        ID = item.ID;
        cost = item.cost;
        buyed = item.buyed;

        Movimiento = item.Movimiento;
        life = item.life;
        powerAttack = item.powerAttack;
        itembuyedCorrectly.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemBuyed()
    {
        if(playerData.buyedItems(ID, cost) && buyed != true)
        {
            itembuyedCorrectly.SetActive(true);
            buyed = true;
        }
    }

    public void ItemWasBuyed()
    {
        itembuyedCorrectly.SetActive(true);
        buyed = true;
    }
}
