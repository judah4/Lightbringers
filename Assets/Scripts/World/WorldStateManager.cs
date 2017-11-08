using System.Collections.Generic;
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
        public Vector3 Position;
        public bool positionSet = false;

        public List<CharacterStat> CharactereStats = new List<CharacterStat>();

        public List<int> MonsterIds = new List<int>();
        public int EncounterId = 1;
        
        public void SetPosition(Vector3 position)
        {
            Position = position;
            positionSet = true;
        }


        public void RecordCurrentWorldScene()
        {
            if(string.IsNullOrEmpty(CurrentScene))
            {
                CurrentScene = "Leeside Village";
            }


            var nextScene = SceneManager.GetActiveScene().name;
            if(CurrentScene != nextScene)
            {
                CurrentScene = SceneManager.GetActiveScene().name;
                //changed scene? move to another starting pos
                positionSet = false;
            }
        }

    }
}
