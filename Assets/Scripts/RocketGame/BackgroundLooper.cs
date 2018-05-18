using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform Background1;
    public Transform Background2;

    private bool WhichOne = true;

    public Transform rocket;

    private float currentHeight = 0;
	
	// Update is called once per frame
	void Update ()
    {
        if (currentHeight < rocket.position.y)
        {
            if (WhichOne)
            {
                Background1.localPosition = new Vector3(0, Background2.localPosition.y + 36, 0);
            }
            else
            {
                Background2.localPosition = new Vector3(0, Background1.localPosition.y + 36, 0);
            }
            currentHeight += 36;

            WhichOne = !WhichOne;
        }
        if (currentHeight > rocket.position.y)
        {
                if (WhichOne)
                {
                    Background2.localPosition = new Vector3(0, Background1.localPosition.y - 36, 0);
                }
                else
                {
                    Background1.localPosition = new Vector3(0, Background2.localPosition.y - 36, 0);
                }
                currentHeight -= 36;

                WhichOne = !WhichOne;
            }
        
	}
}
