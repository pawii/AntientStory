using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTwoPointMovement : TwoPointMovement 
{
	private float yMinPoint;
	private float yMaxPoint;
	private float amplitude;
	private float avgPoint;
	private int directionY;
	
	public BatTwoPointMovement(bool flipX, Transform target, float xMinPoint, float xMaxPoint, 
	                           float yMinPoint, float yMaxPoint) : base(flipX, target, xMinPoint, xMaxPoint)
	{
		this.yMinPoint = yMinPoint;
		this.yMaxPoint = yMaxPoint;
		SetAmplitude();
		avgPoint = yMinPoint + (yMaxPoint - yMinPoint) / 2f;
		directionY = 1;
	}

	public override Vector3 Move()
	{
		Vector3 pos = base.Move();

		if (pos.y >= (avgPoint + amplitude))
		{
			directionY = -1;
		}
		else if (pos.y <= (avgPoint - amplitude))
		{
			directionY = 1;
		}
		else if (pos.y <= (avgPoint + 0.1f) && pos.y >= (avgPoint - 0.1f))
		{
			SetAmplitude();
		}
		pos.y += target.up.y* directionY;

		return pos;
	}

	private void SetAmplitude()
	{
		amplitude = ((yMaxPoint - yMinPoint) / 2f) * Random.Range(0.3f, 1f);
	}
}
