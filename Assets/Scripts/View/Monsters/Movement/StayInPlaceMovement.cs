using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StayInPlaceMovement : Movement
{
	private Vector3 target;
	private Transform triggerTarget;

	public StayInPlaceMovement(bool flipX, Vector3 target, Transform triggerTarget) : base(flipX)
    {
		this.target = target;
		this.triggerTarget = triggerTarget;
	}

	public override Vector3 Move()
	{
		if (target.x - triggerTarget.position.x >= 0)
			FlipX = false;
		else
			FlipX = true;

		return target;
	}
}
