using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{

	public AudioSource audio;
	
	    void Update() {
            transform.RotateAround(new Vector3(218, 50, 160), Vector3.up, 20 * Time.deltaTime);
        }

	private void OnMouseDown()
	{
		audio.Play();
	}
}
