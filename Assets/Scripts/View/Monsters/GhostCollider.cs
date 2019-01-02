using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour 
{
    [SerializeField]
    private int damage = -1;
    [SerializeField]
    private float delay = 0.5f;
	[SerializeField]
    private Animator anim;

    private bool hitRequire;

    private void Awake()
	{
		hitRequire = true;
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.tag == "shield")
		{
			anim.SetTrigger("GetBack");
		}
		
		if (collision.gameObject.tag == "character" && hitRequire)
		{
			hitRequire = false;
			MessageParameters parameters = new MessageParameters(Methods.GetDirection(gameObject), damage);
			GameController.Character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
            StartCoroutine(Delay());
		}
	}

	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(delay);
		hitRequire = true;
	}
}