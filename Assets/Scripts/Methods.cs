using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Methods
{
	public static float Angle(Vector2 start, Vector2 end)
	{
		float angel = (float)Vector2.Angle(start, end);
		angel = (float)angel / 180f * (float)Mathf.PI;
		return angel;
	}

	public static int GetDirection(GameObject gameObject)
	{
		return gameObject.transform.localScale.x > 0 ? 1 : -1;
	}

    public static float RadToGrad(float angel)
    {
        float result = (angel / Mathf.PI) * 180;
        return result;
    }

    public static float GradToRad(float angel)
    {
        angel = angel / 180f * Mathf.PI;
        return angel;
    }
}