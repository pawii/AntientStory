using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesPanel : MonoBehaviour 
{
	private Transform[] hearts = new Transform[5];

	void Start()
	{
		GameController.RefreshLives += Refresh;

		for (int i = 0; i < hearts.Length; i++)
		{
			hearts[i] = transform.GetChild(i);
		}
	}

	void OnDestroy()
	{
		GameController.RefreshLives -= Refresh;
	}

	public void Refresh()
	{
		for (int i = 0; i<hearts.Length; i++)
		{
			if (i < Managers.Player.health) hearts[i].gameObject.SetActive(true);
			else hearts[i].gameObject.SetActive(false);
		}
	}
}
