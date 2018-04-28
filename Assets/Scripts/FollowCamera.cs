using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{	
	[SerializeField] private Camera _camera;
	[SerializeField] private float _angle;
	[SerializeField] private float _height;
	[SerializeField] private float _distance;
	
	// Update is called once per frame
	void Update () {
		_camera.transform.position = new Vector3(transform.position.x, transform.position.y + _height, transform.position.z - _distance);
		_camera.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.right);
	}
}
