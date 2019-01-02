using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster 
{
	[SerializeField]
	private GameObject bullet;
    [SerializeField]
	private float yMinPoint;
    [SerializeField]
	private float yMaxPoint;

    #region Unity lifecycle
    private void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 3;
		damageRate = 3f;

		triggerArea = 10f;

		character = GameController.Character;

		movement = new BatTwoPointMovement(FlipX, transform, xMinPos, xMaxPos, yMinPoint, yMaxPoint);
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
		BulletFactory.CreateBatBullet(transform.position, damage, FlipX ? 1 : -1);
	}

	protected override void SetCalm()
	{
		base.SetCalm();
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new BatTwoPointMovement(FlipX, transform, xMinPos, xMaxPos, yMinPoint, yMaxPoint);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void SetTriggered()
	{
		base.SetTriggered();
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new BatAgressiveMovement(FlipX, transform, character, yMinPoint, yMaxPoint);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void SetAgressive()
	{
		base.SetAgressive();
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new StayInPlaceMovement(FlipX, transform.position, character);
		movement.ChangeFlipX += OnChangeFlipX;
	}
}
