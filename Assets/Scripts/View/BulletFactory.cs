using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletFactory : MonoBehaviour
{
	private static GameObject arrowPrefab;
    private static GameObject batBulletPrefab;
    private static GameObject arrowLightPrefab;

	static BulletFactory()
	{
		arrowPrefab = Resources.Load<GameObject>("Arrow");
		batBulletPrefab = Resources.Load<GameObject>("BatBullet");
		arrowLightPrefab = Resources.Load<GameObject>("ArrowLight");
	}

	public static GameObject CreateArrow(Transform sender, int damage, Vector3 force, string parentTag)
	{
		GameObject arrow = Instantiate(arrowPrefab);
		arrow.transform.position = sender.position;
		arrow.transform.right = force.x < 0 ? -sender.right : sender.right;

		Bullet script = arrow.GetComponent<Bullet>();

		script.Shoot(damage, force, parentTag);

		return arrow;
	}

	public static GameObject CreateArrowWithLight(Transform sender, int damage, Vector3 force, string parentTag)
	{
		GameObject lightParent = CreateArrow(sender, damage, force, parentTag);

		GameObject light = Instantiate(arrowLightPrefab);
        light.transform.parent = lightParent.transform;
        if (force.x < 0f)
        {
            light.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            light.transform.localScale = Vector3.one;
        }
        light.transform.localEulerAngles = Vector3.zero;
        light.transform.localPosition = Vector3.zero;
		return lightParent;
	}

	public static void CreateBatBullet(Vector3 senderPosition, int damage, int direction)
	{
		GameObject bullet = Instantiate(batBulletPrefab);
		bullet.transform.position = senderPosition + (GameController.Character.position - senderPosition) / 2;
		bullet.transform.up = GameController.Character.position - senderPosition;
		MessageParameters parameters = new MessageParameters(direction, damage);
		GameController.Character.SendMessage("OnHit", parameters, SendMessageOptions.DontRequireReceiver);
	}
}