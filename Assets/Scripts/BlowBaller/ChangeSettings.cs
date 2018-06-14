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
        try
        {
            str = input.text;
            input.text = MakeDigitsOnly(str);
            GameMaster.instance.setMinimumValueStart(Convert.ToInt32(str));
        }
        catch (Exception)
        {         
        }
 
    }

    public void changeBasketSpeed()
    {
        try
        {
            str = input.text;
            input.text = MakeDigitsOnly(str);
            GameMaster.instance.setBasketSpeed(Convert.ToInt32(str));
        }
        catch (Exception)
        {
        }

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
