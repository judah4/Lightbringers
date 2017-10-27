using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public void NextScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        print(("tried to switch to " + SceneName));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
