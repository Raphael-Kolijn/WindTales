using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int itemID;
    public string itemName;
    public int speed;

    public void SetItemID(int ID)
    {
        itemID = ID;
    }
    
    public void SetItemName(string name)
    {
        itemName = name;
    }
    
    public virtual void Boost()
    {
        Debug.Log("boooost");
    }
  
}
