using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBullet : MonoBehaviour 
{
	private void Start () 
	{
		StartCoroutine(OnStart());	
	}

	private IEnumerator OnStart()
	{
		yield return new WaitForSeconds(1);
		Destroy(gameObject);
	}
}