using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IntelligenceMovement : Movement
{
	private Transform target;

	public IntelligenceMovement(bool flipX, Transform target) : base(flipX)
    {
		this.target = target;
	}

	public override Vector3 Move()
	{
        Vector3 targetPos = target.position;
		int direction = FlipX ? 1 : -1;
		Vector3 spherePos = targetPos;
		spherePos.y += 0.5f;
		spherePos.x += 0.5f * direction;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spherePos, 0.1f);
        for(int i = 0; i < colliders.Length; i++)
			if (colliders[i].gameObject.layer == LayerMask.NameToLayer("ground"))
				FlipX = !FlipX;

		Vector3 pos = targetPos + target.right * direction;
		return pos;
	}
}
