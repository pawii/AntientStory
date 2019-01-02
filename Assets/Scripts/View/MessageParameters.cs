using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MessageParameters
{
	public readonly int direction;
	public readonly int damage;

	public MessageParameters(int direction, int damage)
	{
		this.direction = direction;
		this.damage = damage;
	}
}
