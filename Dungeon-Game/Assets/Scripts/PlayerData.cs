using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public List<int> items;
    public int dinero;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public bool buyedItems(int ID, int cost)
    {
        if(dinero >= cost)
        {
            items.Add(ID);
            dinero -= cost;
            return true;
        }
        else
        {
            return false;
        }
        

    }
}
