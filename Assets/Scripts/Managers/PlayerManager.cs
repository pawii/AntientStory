using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public static event Action ReloadGame;
	public ManagerStatus status { get; private set; }
	public int health { get; private set; }
	private const int maxHealth = 5;
    public bool HasLigth { get; set; }

	public void StartUp()
	{
		status = ManagerStatus.Initializing;

        health = maxHealth;
        HasLigth = false;

		status = ManagerStatus.Started;
	}

	public void ChangeHealth(int value)
	{
		health += value;
		if (health > maxHealth) health = maxHealth;
		else if (health < 0) health = 0;
		if (health == 0)
        {
            ReloadGame();
        }
	}

	public void Reload()
	{
        health = maxHealth;
	}
}