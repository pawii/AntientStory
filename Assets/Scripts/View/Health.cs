using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private float amplitude = 0.5f;
	private int direction;
    private float startY;

    private void Start()
	{
		direction = 1;
		startY = transform.position.y;
	}

    private void Update()
	{
        Vector3 pos = transform.position;
		if (pos.y >= startY + amplitude / 2)
			direction = -1;
		else if (pos.y <= startY - amplitude / 2)
			direction = 1;
		Vector3 target = pos + transform.up * direction;
		transform.position = Vector3.MoveTowards(pos, target, speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "character")
		{
			GameController.ChangeHealth(1);
			Destroy(gameObject);
		}
	}
}
