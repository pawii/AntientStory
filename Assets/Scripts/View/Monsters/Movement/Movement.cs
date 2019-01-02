using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Movement
{
    private bool flipX;
    public event Action<bool> ChangeFlipX;
    protected bool FlipX
    {
        get { return flipX; }
        set
        {
            flipX = value;
            ChangeFlipX(value);
        }
    }

    public Movement(bool flipX)
    {
        this.flipX = flipX;
    }

	public virtual Vector3 Move()
	{
		return Vector3.zero;
	}
}
