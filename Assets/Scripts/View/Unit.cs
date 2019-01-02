using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Unit : MonoBehaviour 
{
	private bool flipX;
	public bool FlipX
	{
		get { return flipX; }
		set
		{
			if (value == flipX)
				return;
			else
			{
				Vector3 newScale = transform.localScale;
				newScale.x *= -1;
				transform.localScale = newScale;

				flipX = !flipX;
			}
		}
	}
}
