using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{

	[SerializeField][Range(1, 100)] private float _rotationSpeed;
	public Animator Animator;
	
	void Update () {
		// Rotate the object around its local X axis at 1 degree per second
		transform.Rotate((Vector3.up * _rotationSpeed) * Time.deltaTime);
	}

	public void AnimationEnded()
	{
		Animator.SetBool("PlayAnimation", false);
	}
}
