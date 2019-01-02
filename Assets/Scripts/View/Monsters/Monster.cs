using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Monster : MonoBehaviour// : Unit 
{
    private bool flipX;
    protected bool FlipX
    {
        get { return flipX; }
        set
        {
            if (value == flipX)
                return;
            else
            {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;

                flipX = !flipX;
            }
        }
    }

    protected int health;
	protected float speed;
	protected int damage;
	protected float damageArea;
	protected float damageRate;

	protected MonsterState state;

	protected float triggerArea;

	protected Transform character;

	public float xMinPos;
	public float xMaxPos;
	protected Movement movement;

	protected float getDamagePower;
	[SerializeField]
	private Rigidbody2D rb;

    private Coroutine damageCoroutine;


	#region Unity lifecycle
	void Awake()
	{
		Messenger.AddListener(GameEvent.CHARACTER_SEEMED, OnCharacterSeemed);
		Messenger.AddListener(GameEvent.CHARACTER_HIDED, OnCharacterHided);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CHARACTER_SEEMED, OnCharacterSeemed);
        Messenger.RemoveListener(GameEvent.CHARACTER_HIDED, OnCharacterHided);
    }

    void Start()
	{
		state = MonsterState.Find;
    }

	// ПАТТЕРН "ШАБЛОННЫЙ МЕТОД"
	void Update()
	{
		if (state == MonsterState.Find)
		{
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, triggerArea);
            for(int i = 0; i < colliders.Length; i++)
				if (colliders[i].gameObject.tag == "character")
				{
					SetTriggered();
				}
		}
		else
		{
			if (state == MonsterState.Triggered)
			{
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageArea);
                for (int i = 0; i < colliders.Length; i++)
                    if (colliders[i].gameObject.tag == "character")
					{
						SetAgressive();
						break;
					}
			}
		}
		Move();
	}
	#endregion

	public void OnHit(MessageParameters parameters)
	{
		health--;
		if (health < 1)
			Destroy(gameObject);
		Vector3 getDamageForce = new Vector3(0.1f, 1, 0);
		getDamageForce.x *= parameters.direction;
		if (rb != null)
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(getDamagePower * getDamageForce, ForceMode2D.Impulse);
		}

		if (state == MonsterState.Calm || state == MonsterState.Find)
            SetTriggered();
	}

	public void OnCharacterHided()
	{
		if(damageCoroutine != null)
			StopCoroutine(damageCoroutine);
		SetCalm();
	}

	public void OnCharacterSeemed()
	{
		state = MonsterState.Find;
	}

	protected void Move()
	{
		if(state != MonsterState.GetHit)
			transform.position = Vector3.Lerp(transform.position, movement.Move(), speed * Time.deltaTime);
	}
    
	private IEnumerator Damaging()
	{
		Attack();
		yield return new WaitForSeconds(damageRate);
		SetTriggered();
	}

	protected virtual void Attack()
	{
	}

	protected virtual void SetCalm()
	{
		state = MonsterState.Calm;
	}

	protected virtual void SetTriggered()
	{
		state = MonsterState.Triggered;
	}

	protected virtual void SetAgressive()
	{
		state = MonsterState.Attacked;
		damageCoroutine = StartCoroutine(Damaging());
	}

	protected void OnChangeFlipX(bool flipX)
	{
		FlipX = flipX;
	}
}