using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{
	[SerializeField]
	private Text NotificationText;
	[SerializeField]
	private GameObject background;

	void Awake()
	{
		GameController.ShowNotification += OnShowNotificatin;
		GameController.RemoveNotification += OnRemoveNotification;
	}

	void Destroy()
	{
		GameController.ShowNotification -= OnShowNotificatin;
		GameController.RemoveNotification -= OnRemoveNotification;
	}

	void Start()
	{
		NotificationText.gameObject.SetActive(false);
	}

	public void OnShowNotificatin(string notification)
	{
		NotificationText.text = notification;
		NotificationText.gameObject.SetActive(true);
		background.SetActive(true);
	}

	public void OnRemoveNotification()
	{
		NotificationText.gameObject.SetActive(false);
		NotificationText.text = string.Empty;
		background.SetActive(false);
	}
}
