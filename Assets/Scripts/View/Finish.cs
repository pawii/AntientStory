using System.Collections;
using System;
using UnityEngine;

public class Finish : MonoBehaviour 
{
    public static event Action FinishLevel;
	private bool locker = false;
    

    private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!locker && collider.gameObject.tag == "character")
		{
            FinishLevel();
			StartCoroutine(Delay());
		}
	}

    private IEnumerator Delay()
	{
		locker = true;
		yield return new WaitForSeconds(1);
		locker = false;
	}
}
