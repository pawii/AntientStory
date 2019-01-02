using UnityEngine;
using System;
using System.Collections;

namespace Test
{
    public class MissionManager : MonoBehaviour, IGameManager
    {
        public static event Action OnLevelLoaded;

        public ManagerStatus status { get; private set; }

        [SerializeField]
        private int maxLevel = 3;

        private int currentLevel;
        private GameObject levelGameObject;


        #region Public Methods

        public void StartUp()
        {
            status = ManagerStatus.Initializing;

            currentLevel = 0;

            status = ManagerStatus.Started;
        }

        public void LoadNextLevel()
        {

        }

        public void LodaLevel(int level)
        {
            if (level <= maxLevel && level > 0)
            {
                currentLevel = level;
                RestartCurrent();
            }
            else
            {
                Debug.LogError("Level doesn't exists");
            }
        }

        public void RestartCurrent()
        {
            if (levelGameObject != null)
            {
                Destroy(levelGameObject);
                GC.Collect();
            }

            levelGameObject = Resources.Load<GameObject>("Level" + currentLevel);
            levelGameObject = Instantiate(levelGameObject);
            levelGameObject.transform.localPosition = Vector3.zero;

            StartCoroutine(WorkImulation());
        }

        #endregion

        #region Private Methods

        private IEnumerator WorkImulation()
        {
            yield return new WaitForSeconds(1);
            OnLevelLoaded?.Invoke();
        }

        #endregion
    }
}