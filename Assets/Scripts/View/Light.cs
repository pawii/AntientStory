using System;
using UnityEngine;

public class Light : MonoBehaviour 
{
    public static event Action AddLight;
    
	public void Operate()
	{
        if (AddLight != null)
        {
            AddLight();
        }
	}
}
