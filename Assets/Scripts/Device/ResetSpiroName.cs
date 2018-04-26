using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSpiroName : MonoBehaviour {

public void ResetSpiro()
    {
        PlayerPrefs.SetString("adress", "");
    }
}
