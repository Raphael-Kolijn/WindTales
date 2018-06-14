using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ChangeSettings : MonoBehaviour
{

    public InputField input;
    private string str;

    public void changeStartValue()
    {
        str = input.text;
        input.text = MakeDigitsOnly(str);
        GameMaster.instance.setMinimumValueStart(Convert.ToInt32(str));
    }

    public void changeBasketSpeed()
    {
        str = input.text;
        input.text = MakeDigitsOnly(str);
        GameMaster.instance.setBasketSpeed(Convert.ToInt32(str));
    }
    private string MakeDigitsOnly(string str)
    {
        string temp = "";
        foreach (char i in str)
        {
            if (i >= '0' && i <= '9')
            {
                temp += i;
            }
        }

        return temp;
    }
}
