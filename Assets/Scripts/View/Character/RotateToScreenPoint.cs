using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Profiling;

public class RotateToScreenPoint : MonoBehaviour
{
    private float offset;

    #region Unity lifecycle
    private void Start()
	{
		offset = transform.localEulerAngles.z;
	}

    private void LateUpdate()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 newRight = mousePos - transform.position;

		if (!Player.instance.isLookingToTheRight)
			newRight.x = newRight.x * -1;

		Vector3 newAngel = Vector3.zero;

		if (newRight.y >= 0)
			newAngel.z = Vector2.Angle(Vector2.right, newRight);
		else
			newAngel.z = -Vector2.Angle(Vector2.right, newRight);

		newAngel.z += offset;
		transform.localEulerAngles = newAngel;
	}
	#endregion
}
