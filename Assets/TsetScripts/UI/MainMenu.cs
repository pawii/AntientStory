using System;
using UnityEngine;

namespace Test
{
    public class MainMenu : MonoBehaviour
    {
        public static event Action<int> OnLevelPicked; 

        [SerializeField]
        private GameObject startPage;
        [SerializeField]
        private GameObject levelPage;
        [SerializeField]
        private GameObject optionsPage;
        

        #region Start page Methoods

        public void OpenLevelPage()
        {
            startPage.SetActive(false);
            levelPage.SetActive(true);
        }

        public void OpenOptionsPage()
        {
            startPage.SetActive(false);
            optionsPage.SetActive(true);
        }

        public void Exit()
        {
            Application.Quit();
        }

        #endregion

        public void BackToStartPage()
        {
            optionsPage.SetActive(false);
            levelPage.SetActive(false);
            startPage.SetActive(true);
        }

        #region Level Page Methoods

        public void LoadLevel(int level)
        {
            OnLevelPicked(level);
        }

        #endregion
    }
}