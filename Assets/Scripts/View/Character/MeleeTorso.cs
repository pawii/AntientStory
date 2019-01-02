using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeleeTorso : MonoBehaviour
{
	[SerializeField]
	private Animator anim;

	public static event Action<bool> FastSpeedChanged;

    #region Unity lifecycle
    private void Update()
	{
		if (Input.GetMouseButtonDown(0))
			anim.SetTrigger("Attack");

		if (Input.GetMouseButtonDown(1))
		{
			anim.SetBool("Protect", true);
			FastSpeedChanged(false);
		}

		if (Input.GetMouseButtonUp(1))
		{
			anim.SetBool("Protect", false);
			FastSpeedChanged(true);
		}
	}
	#endregion
}
