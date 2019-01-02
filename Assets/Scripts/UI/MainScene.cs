using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelMenu;
    [SerializeField]
    private GameObject optionsMenu;

    #region Main Menu Methoods

    public void OpenLevelMenu()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion

    public void BackToMainMenu()
    {
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    #region Level Menu Methoods

    public void LoadLevel(int level)
    {
        GameController.LoadLevel(level);
    }

    #endregion
}
