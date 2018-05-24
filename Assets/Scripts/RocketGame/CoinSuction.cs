using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSuction : MonoBehaviour
{
    public MagnetOrbit magnetOrbit;
    float speed = 15.0f;

	void Update ()
    {
        while (magnetOrbit.flowRate < -100)
        {
            magnetOrbit.target.transform.position = Vector3.MoveTowards(magnetOrbit.target.transform.position, transform.position, speed * Time.deltaTime);
        }
	}
}
