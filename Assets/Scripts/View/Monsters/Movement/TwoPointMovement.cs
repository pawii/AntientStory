using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwoPointMovement : Movement
{
	protected Transform target;
	private float xMinPoint;
	private float xMaxPoint;
	int directionX;

	public TwoPointMovement(bool flipX, Transform target, float xMinPoint, float xMaxPoint) : base(flipX)
    {
		this.target = target;
		this.xMinPoint = xMinPoint;
		this.xMaxPoint = xMaxPoint;
		directionX = flipX ? 1 : -1;
	}

	public override Vector3 Move()
	{
        Vector3 targetPos = target.position;
		if (targetPos.x <= xMinPoint)
		{
			directionX = 1;
			FlipX = true;
		}
		else if (targetPos.x >= xMaxPoint)
		{
			directionX = -1;
			FlipX = false;
		}

		Vector3 pos = targetPos + target.right * directionX;
		return pos;
	}
}
