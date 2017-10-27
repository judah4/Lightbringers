using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour {

    public Transform PauseMenu;

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        print("Active scene is '" + SceneName + "'.");
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main Menu");
        print(("tried to switch to " + "SceneName"));
    }



    public void ExitGame()
    {
        Application.Quit();
    }
}
