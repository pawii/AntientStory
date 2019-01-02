using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Archer : Monster 
{
	[SerializeField]
	private Animator anim;
	[SerializeField] 
	private Transform arrow;

    private float bowAngel;
    private float maxVelocity;

    #region Unity lifecycle
    private void Awake()
	{
		health = 3;
		speed = 3f;
		damage = -1;
		damageArea = 7f;
		damageRate = 2f;

		triggerArea = 7;

		character = GameController.Character;

		movement = new TwoPointMovement(FlipX, transform, xMinPos, xMaxPos);
		movement.ChangeFlipX += OnChangeFlipX;

		getDamagePower = 5;


        // ДЛЯ ВТОРОЙ ЧАСТИ КЛАССА
        headOffset = head.localEulerAngles.z;
        armOffset = arm.localEulerAngles.z;
        maxVelocity = 10f;
    }

    private void OnDestroy()
	{
		movement.ChangeFlipX -= OnChangeFlipX;
	}
    #endregion

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
		anim.SetBool("attack", false);
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new AgressiveMovement(FlipX, transform, character);
		movement.ChangeFlipX += OnChangeFlipX;
	}

	protected override void SetAgressive()
	{
		base.SetAgressive();
		anim.SetBool("attack", true);
		movement.ChangeFlipX -= OnChangeFlipX;
		movement = new StayInPlaceMovement(FlipX, transform.position, character);
		movement.ChangeFlipX += OnChangeFlipX;
	}

    protected override void Attack()
	{
        if (bowAngel != 0)
        {
            Vector3 characterPos = character.position;
            Vector3 arrowPos = arrow.position;

            Vector3 direction = FlipX ? arrow.right : -arrow.right;

            // ВЫСЧИТЫВАЕМ V(СИЛУ)
            float x = characterPos.x - arrowPos.x;
            if(x < 0)
            {
                x = x * -1;
                bowAngel = 180 - bowAngel;
            }
            float y = 0;
            float h = arrowPos.y - characterPos.y; // ВОЗМОЖНО БАГ
            float alpha = Methods.GradToRad(bowAngel); // ВОЗМОЖНО БАГ

            float velocity = Mathf.Sqrt(-Physics2D.gravity.magnitude * Mathf.Pow(x, 2) / ((y - Mathf.Tan(alpha) * x - h) * 2 * Mathf.Pow(Mathf.Cos(alpha), 2)));
            velocity = Mathf.Clamp(velocity, 0, maxVelocity);

            direction *= velocity;
            BulletFactory.CreateArrow(arrow, damage, direction, tag);
        }
    }
}

// ОТВЕЧАЕТ ЗА ВРАЩЕНИЕ ЛУКА
public partial class Archer : Monster
{
    private float headOffset;
    private float armOffset;

    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform arm;

    void LateUpdate()
    {
        if (state == MonsterState.Attacked)
        {
            Vector3 toTarget = new Vector3(character.position.x - arrow.position.x,
                character.position.y - arrow.position.y, 0);
            float absX = Mathf.Abs(toTarget.x);
            if (absX <= damageArea + 1 && absX >= 1)
            {
                Vector3 newAngel = Vector3.zero;

                // НАПРАВЛЯЕМ УГОЛ ПРЯМО НА ЦЕЛЬ
                newAngel.z = Vector3.Angle(Vector3.right, toTarget);

                // РАССТАВЛЯЕМ ЗНАКИ
                if (toTarget.y < 0)
                    newAngel.z = newAngel.z * -1;

                // ВЫЧИСЛЯЕМ ОФФСЕТ
                // ЕСЛИ toTarget.x = damageArea, ТО ОФФСЕТ = 45
                // ЕСЛИ toTarget.x = 0, ТО ОФФСЕТ = 0
                float offset = Mathf.Abs(toTarget.x) / damageArea * 45;

                // ОГРАНИЧИВАЕМ ОФФСЕТ
                // ЕСЛИ |УГОЛ|=90, ОФФСЕТ *= 0
                // ЕСЛИ |УГОЛ|=0, ОФФСЕТ *= 1
                offset = offset * (90 - Mathf.Abs(newAngel.z)) / 90;

                newAngel.z += offset;

                bowAngel = newAngel.z;

                Vector3 headAngel = newAngel;
                headAngel.z += headOffset;
                Vector3 armAngel = newAngel;
                armAngel.z += armOffset;

                // ЕСЛИ toTarget.x < 0, ТО ЗЕРКАЛЬНО ОТРАЖАЕМ УГОЛ
                if (toTarget.x < 0)
                {
                    armAngel.z = armAngel.z * -1;
                    headAngel.z = (headAngel.z * -1) - 180;
                }

                arm.localEulerAngles = armAngel;
                head.localEulerAngles = headAngel;
            }
        }
    }
}