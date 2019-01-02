using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour 
{
	private int damage = 1;
    private bool dontHit = false;

	#region Unity lifecycle
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag != "character" && !dontHit)
		{
			int direction = !Player.instance.isLookingToTheRight ? -1 : 1;
			collision.gameObject.SendMessage("OnHit", new MessageParameters(direction, damage));
			StartCoroutine(Delay());
		}
	}
    #endregion

    private IEnumerator Delay()
	{
		dontHit = true;
		yield return new WaitForSeconds(0.5f);
		dontHit = false;
	}
}
