using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : Item {

    private int bonusSpeed = 35;

    public int GetItemID()
    {
        return itemID;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public int AddSpeed()
    {
        return bonusSpeed;
    }
}
