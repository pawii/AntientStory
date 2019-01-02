using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(MissionManager))]
public class Managers : MonoBehaviour 
{
	public static PlayerManager Player { get; private set; }
	public static MissionManager Mission { get; private set; }

	private List<IGameManager> managers;

    public static event Action ManagersStarted;

	#region Unity lifecycle
	void Awake()
	{
        Player = GetComponent<PlayerManager>();
        Mission = GetComponent<MissionManager>();

		managers = new List<IGameManager>();
		managers.Add(Player);
		managers.Add(Mission);
		StartCoroutine(StartupManagers());
	}
	#endregion

	private IEnumerator StartupManagers()
	{
		for(int i = 0; i < managers.Count; i++)
			managers[i].StartUp();

		yield return null;

		int numModules = managers.Count;
		int numReady = 0;

		while (numReady < numModules)
		{
			int lastReady = numReady;
			numReady = 0;

			for(int i = 0; i < managers.Count; i++)
				if (managers[i].status == ManagerStatus.Started)
					numReady++;

			if (numReady > lastReady)
			{
				Debug.Log("Progress: " + numReady + "/" + numModules);
			}

			yield return null;
		}
        ManagersStarted();
	}
}
