using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Text notificationText;
        [SerializeField]
        private GameObject background;
        [SerializeField]
        private Transform[] healthUI = new Transform[5];

        private GameObject mainMenu;
        

        #region Public methods

        public void ShowNotificatin(string notification)
        {
            notificationText.text = notification;
            notificationText.gameObject.SetActive(true);
            background.SetActive(true);
        }

        public void RemoveNotification()
        {
            notificationText.gameObject.SetActive(false);
            notificationText.text = string.Empty;
            background.SetActive(false);
        }

        public void RefreshHealthPanel(int healthCount)
        {
            for (int i = 0; i < healthUI.Length; i++)
            {
                if (i < healthCount)
                {
                    healthUI[i].gameObject.SetActive(true);
                }
                else
                {
                    healthUI[i].gameObject.SetActive(false);
                }
            }
        }

        public void OpenMainMenu()
        {
            mainMenu = Instantiate(Resources.Load<GameObject>("MainMenu"));
        }

        public void CloseMainMenu()
        {
            Destroy(mainMenu);
        }

        #endregion
    }
}
