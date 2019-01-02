using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAgressiveMovement : AgressiveMovement
{
	private float yMinPoint;
	private float yMaxPoint;
	private float amplitude;
	private float avgPoint;
	private int direction;

	public BatAgressiveMovement(bool flipX, Transform target, Transform triggertarget,
								float yMinPoint, float yMaxPoint) : base(flipX, target, triggertarget)
	{
		this.yMinPoint = yMinPoint;
		this.yMaxPoint = yMaxPoint;
		SetAmplitude();
		avgPoint = yMinPoint + (yMaxPoint - yMinPoint) / 2f;
		direction = 1;
	}

	public override Vector3 Move()
	{
		Vector3 pos = base.Move();

		if (pos.y >= (avgPoint + amplitude))
		{
			direction = -1;
		}
		else if (pos.y <= (avgPoint - amplitude))
		{
			direction = 1;
		}
		else if (pos.y <= (avgPoint + 0.1f) && pos.y >= (avgPoint - 0.1f))
		{
			SetAmplitude();
		}
		pos.y += target.up.y * direction;

		return pos;
	}

	private void SetAmplitude()
	{
		amplitude = ((yMaxPoint - yMinPoint) / 2f) * Random.Range(0.3f, 1f);
	}
}
