using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{

	public AudioSource audio;
	public ParticleSystem particles;
	
	void Update()
	{
            transform.RotateAround(new Vector3(218, 50, 157), Vector3.up, 20 * Time.deltaTime);
    }

	private void OnMouseDown()
	{
		audio.Play();
	}

	private void OnCollisionEnter(Collision other)
	{
		particles.Play();
		audio.Play();
	}
}
