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
        //SceneManager.LoadScene(SceneName);
        print("Tried to restart " + SceneName + ".");
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        print(("tried to switch to " + "SceneName"));
    }

    public void ExitGame()
    {
        //add function to quicksave the game
        Application.Quit();
    }
}
