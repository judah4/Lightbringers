using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Encounters
{
    public class BackgroundLoader : MonoBehaviour
    {

        public string SceneName;

        // Use this for initialization
        void Start () {

            for (int cnt = 0; cnt < SceneManager.sceneCount; cnt++)
            {
                if(SceneManager.GetSceneAt(cnt).name == SceneName)
                    return;
            }

            
		    SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

    }
}
