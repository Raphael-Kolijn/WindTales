using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class changeMovementBasket : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropDown;


    public void changeMovement()
    {
        if (dropDown.options[dropDown.value].text == "On")
        {
            GameMaster.instance.setMovingBasket(true);
        }

        else if (dropDown.options[dropDown.value].text == "Off")
        {
            GameMaster.instance.setMovingBasket(false);
        }
    }

}
