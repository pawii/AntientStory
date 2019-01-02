using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour 
{
	private string parentTag;
	[SerializeField]
	private Rigidbody2D rb;
	private bool hit = true;

	private Vector3 force;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 startRot;
    private Vector3 attachOffset;
    private Transform target;
    private int rotDirection;

    private int damage;

    #region Unity lifecycle
    private void Update()
	{
		if (!hit)
		{
			float percent = (transform.position.x - startPos.x) / ((endPos.x - startPos.x) / 2);
			Vector3 curRot = Vector3.zero;
			curRot.x = startRot.x;
			if (rotDirection == 1)
				curRot.y = startRot.y - startRot.y * percent;
			else
				curRot.y = startRot.y + startRot.y * percent;
			transform.right = curRot;
		}
	}

    private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == parentTag)
			return;

		if (!hit && collider.gameObject.layer != LayerMask.NameToLayer("dont hit"))
		{
			hit = true;
			Destroy(rb);

			transform.parent = collider.transform;

			int direction = force.x < 0 ? -1 : 1;
			MessageParameters parameters = new MessageParameters(direction, damage);
			if (collider.gameObject.tag != "shield")
			{
				collider.gameObject.SendMessageUpwards("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
			}
			StartCoroutine(Hitting());
		}
	}
    #endregion

    private IEnumerator Hitting()
	{
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}

    private void CalculateData()
	{
		startRot = transform.right;
		startPos = transform.position;
		float time = (2f * (float)force.magnitude * (float)Mathf.Sin((float)Methods.Angle(new Vector2(1, 0), force))) / (float)Physics2D.gravity.magnitude;

        // БАГ - FORCE.Y = 0
        if (time == 0f)
        {
            time++;
        }
		// БАГ - FORCE.Y = 0

		endPos = startPos;
		endPos.x += Vector2.Dot(force, new Vector2(1, 0)) * time;
		rotDirection = startRot.y < 0 ? -1 : 1;
	}

	public void Shoot(int damage, Vector2 force, string parentTag)
	{
		this.damage = damage;
		this.force = force;
		this.parentTag = parentTag;
		CalculateData();
		rb.AddForce(force, ForceMode2D.Impulse);
		hit = false;
	}
}