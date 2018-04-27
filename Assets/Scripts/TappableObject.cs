using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TappableObject : MonoBehaviour
{

	// Trigger when player comes close to the object
	private CapsuleCollider _enterTrigger;	
	[SerializeField] [Range(1,7)] private float _enterRadius;
	
	// Use this for initialization
	void Start ()
	{
		_enterTrigger = gameObject.AddComponent<CapsuleCollider>();
		_enterTrigger.radius = _enterRadius;
		_enterTrigger.isTrigger = true;
		_enterTrigger.center = new Vector3(0,0,0);
	}
	
	// Draw the trigger radius in editor
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _enterRadius);
	}
}
