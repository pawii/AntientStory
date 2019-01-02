using System;
using UnityEngine;

namespace Test
{
    public class PlayerManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }
        public int Health { get; private set; }
        public bool HasLigth { get; set; }

        [SerializeField]
        private int maxHealth = 5;


        public void StartUp()
        {
            status = ManagerStatus.Initializing;

            Health = maxHealth;
            HasLigth = false;

            status = ManagerStatus.Started;
        }
    }
}