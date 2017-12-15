using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Encounters
{
    public class BackgroundLoader : MonoBehaviour
    {

        public string SceneName;

        // Use this for initialization
        void Start () {

            //load from World manager

            if(World.WorldStateManager.Instance.CurrentScene == "Route 1")
            {
                SceneName = "Forest Battle";
            }
            else if(World.WorldStateManager.Instance.CurrentScene == "Slime Cave")
            {
                SceneName = "Cave Battle";
            }
            else if(World.WorldStateManager.Instance.CurrentScene == "Castle Interior")
            {
                SceneName = "CastleBattle";
            }


            for (int cnt = 0; cnt < SceneManager.sceneCount; cnt++)
            {
                if(SceneManager.GetSceneAt(cnt).name == SceneName)
                    return;
            }

            
		    SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

    }
}
