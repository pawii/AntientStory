using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour 
{
	public static Transform Character { get; private set;}
	private static int startLevel = 1;
    
	public static event Action<string> ShowNotification;
	public static event Action RemoveNotification;
	public static event Action RefreshLives;

	private void Start()
	{
        Managers.ManagersStarted += OnManagersStarted;
        Managers.Mission.LevelLoad += OnLevelLoad;
        Finish.FinishLevel += GameController_OnFinishLevel;
        PlayerManager.ReloadGame += () => { StartCoroutine(ReloadGame()); };
    }

	private void OnDestroy()
	{
        Managers.ManagersStarted -= OnManagersStarted;
        Managers.Mission.LevelLoad -= OnLevelLoad;
        Finish.FinishLevel -= GameController_OnFinishLevel;
        PlayerManager.ReloadGame -= () => { StartCoroutine(ReloadGame()); };
    }

	public static IEnumerator ReloadGame()
	{
		ShowNotification("LEVEL FAILED");
		yield return new WaitForSeconds(2);
		Managers.Mission.RestartCurrent();
		Managers.Player.Reload();
		RefreshLives();
		RemoveNotification();
	}

	public static void ChangeHealth(int value)
	{
		Managers.Player.ChangeHealth(value);
		RefreshLives();
	}

	private void OnManagersStarted()
    {
        Managers.Mission.LoadMainScene();
	}

    public static void LoadLevel(int level)
    {
        ShowNotification("PLEASE, WAIT");

        Managers.Mission.LoadLevel(level);
    }

	public static void GameController_OnFinishLevel()
	{
		ShowNotification("PLEASE, WAIT");

		Managers.Mission.GoNext();
	}

	public static void OnLevelLoad()
	{
		RemoveNotification();
	}

	public static void OnGameComplete()
	{
		ShowNotification("GAME END");
        AudioListener.volume = 0;
    }
}
