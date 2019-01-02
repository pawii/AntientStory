using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float speed = 2f;

	void Update()
	{
		Vector3 targetPos = target.position;
		targetPos.z = -10;
		transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
	}
}
