using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.World
{
    public class WorldStateManager : MonoBehaviour
    {
        private static WorldStateManager _instance;
        public static WorldStateManager Instance { get {
            if (_instance == null)
            {
                _instance = new GameObject("WorldState Manager").AddComponent<WorldStateManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        } }

        public string CurrentScene;

        public void RecordCurrentWorldScene()
        {
            CurrentScene = SceneManager.GetActiveScene().name;
        }

    }
}
