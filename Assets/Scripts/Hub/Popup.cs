using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{

	[SerializeField][Range(1, 100)] private float _rotationSpeed;
	private Vector3 originalScale;
	Vector3 destinationScale = new Vector3(7f, 7f, 7f);

	void Start()
	{
		originalScale = transform.localScale;
	}
	
	void Update () {
		transform.Rotate((Vector3.up * _rotationSpeed) * Time.deltaTime);
	}

	public void IncreasePopup()
	{
		StartCoroutine(IncreaseSize(0.5f));		
	}

	IEnumerator IncreaseSize(float time)
	{
         float currentTime = 0.0f;
         
         do
         {
             transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
             currentTime += Time.deltaTime;
             yield return null;
         } while (currentTime <= time);

		StartCoroutine(DecreaseSize(0.5f));
	}	
	
	IEnumerator DecreaseSize(float time)
	{
		float currentTime = 0.0f;
         
		do
		{
			transform.localScale = Vector3.Lerp(destinationScale, originalScale, currentTime / time);
			currentTime += Time.deltaTime;
			yield return null;
		} while (currentTime <= time);
         
	}	
}
