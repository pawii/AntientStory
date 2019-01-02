using UnityEngine;

namespace Test
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private UIController UIController;
        [SerializeField]
        private Managers managers;


        #region Unity lifecycle

        private void Start()
        {
            UIController.ShowNotificatin("Plaese, wait");

            managers.ManagersStarted += GameController_OnManagersStarted;
            MissionManager.OnLevelLoaded += GameController_OnLevelLoaded;
            MainMenu.OnLevelPicked += GameController_OnLevelPicked;
        }

        private void OnDestroy()
        {
            managers.ManagersStarted -= GameController_OnManagersStarted;
            MissionManager.OnLevelLoaded -= GameController_OnLevelLoaded;
            MainMenu.OnLevelPicked -= GameController_OnLevelPicked;
        }

        #endregion

        #region Event handlers

        private void GameController_OnManagersStarted()
        {
            UIController.RemoveNotification();
            UIController.OpenMainMenu();
        }

        private void GameController_OnLevelPicked(int level)
        {
            UIController.CloseMainMenu();
            UIController.ShowNotificatin("Plaese, wait");
            managers.Mission.LodaLevel(level);
        }

        private void GameController_OnLevelLoaded()
        {
            UIController.RemoveNotification();
        }

        #endregion
    }
}
