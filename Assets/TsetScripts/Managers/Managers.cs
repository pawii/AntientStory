using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Test
{
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(MissionManager))]
    public class Managers : MonoBehaviour
    {
        public PlayerManager Player { get; private set; }
        public MissionManager Mission { get; private set; }

        private List<IGameManager> managers;

        public event Action ManagersStarted;


        private void Awake()
        {
            Player = GetComponent<PlayerManager>();
            Mission = GetComponent<MissionManager>();

            managers = new List<IGameManager>();
            managers.Add(Player);
            managers.Add(Mission);
            StartCoroutine(StartupManagers());
        }
        
        private IEnumerator StartupManagers()
        {
            for (int i = 0; i < managers.Count; i++)
            { managers[i].StartUp(); }

            yield return null;

            int numModules = managers.Count;
            int numReady = 0;

            while (numReady < numModules)
            {
                int lastReady = numReady;
                numReady = 0;

                for (int i = 0; i < managers.Count; i++)
                {
                    if (managers[i].status == ManagerStatus.Started)
                    { numReady++; }
                }

                if (numReady > lastReady)
                {
                    Debug.Log("Progress: " + numReady + "/" + numModules);
                }

                yield return null;
            }

            StartCoroutine(WorkImulation());
        }

        private IEnumerator WorkImulation()
        {
            yield return new WaitForSeconds(1);
            ManagersStarted?.Invoke();
        }
    }
}