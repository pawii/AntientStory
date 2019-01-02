using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster 
{
	[SerializeField]
	private Animator anim;

    #region Unity lifecycle
    private void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 1;
		damageRate = 1f;

		triggerArea = 3f;

		character = GameController.Character;

		movement = new TwoPointMovement(FlipX, transform, xMinPos, xMaxPos);
		movement.ChangeFlipX += OnChangeFlipX;

		getDamagePower = 5;
	}

    private void OnDestroy()
	{
		movement.ChangeFlipX -= OnChangeFlipX;
	}
    #endregion

    protected override void Attack()
	{
		anim.SetTrigger("Attack");
	}

	protected override void SetCalm()
	{
		base.SetCalm();
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new TwoPointMovement(FlipX, transform, xMinPos, xMaxPos);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void SetTriggered()
	{
		base.SetTriggered();
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new AgressiveMovement(this, transform, character);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void SetAgressive()
	{
		base.SetAgressive();
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new StayInPlaceMovement(this, transform.position, character);
		movement.ChangeFlipX += OnChangeFlipX;
	}
}
