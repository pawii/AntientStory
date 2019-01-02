using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour 
{
	private bool isHided;

    private void Awake()
	{
		isHided = false;
	}

	public void Operate()
	{
		isHided = !isHided;
		if (isHided)
			Messenger.Broadcast(GameEvent.CHARACTER_HIDED);
		else
			Messenger.Broadcast(GameEvent.CHARACTER_SEEMED);
	}
}