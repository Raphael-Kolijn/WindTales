using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int itemID;
    public string itemName;

    public void SetItemID(int ID)
    {
        itemID = ID;
    }
    
    public void SetItemName(string name)
    {
        itemName = name;
    }
  
}
