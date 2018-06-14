using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TappableObject : MonoBehaviour
{

	// Trigger when player comes close to the object
	private CapsuleCollider _enterTrigger;	
	[SerializeField] [Range(1,20)] public float _enterRadius;
	[SerializeField] private GameObject _ui;
	[HideInInspector] public GameObject _uiInstance = null;
	public HubAudioManager AudioManager;
	
	// Use this for initialization
	void Start ()
	{
		InitialiseTrigger();
	}
	
	// Draw the trigger radius in editor
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, _enterRadius);
	}

	public void InitialiseTrigger()
	{
		_uiInstance = Instantiate(_ui);
		_uiInstance.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
		_uiInstance.SetActive(false);
		
		_enterTrigger = gameObject.AddComponent<CapsuleCollider>();
		_enterTrigger.radius = _enterRadius;
		_enterTrigger.isTrigger = true;
		_enterTrigger.center = new Vector3(0,0,0);
	}

	public abstract void OpenUi();

	public void CloseUi()
	{
		_uiInstance.SetActive(false);
	}

	private void OnMouseDown()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, _enterRadius);

		foreach (var collider1 in colliders)
		{
			if (collider1.name.Equals("Player"))
			{
				OpenUi();			
			}
		}	
	}
}
