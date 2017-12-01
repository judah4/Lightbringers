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

		public List<CharacterStats> CharacterStats = new List<CharacterStats>();


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

		private void Start()
		{
			SetupCharacters();
		}

		void SetupCharacters()
		{
			var pos = 0;

			SpawnPlayer(CharacterClass.Warrior, pos++);
			SpawnPlayer(CharacterClass.Wizard, pos++);
			SpawnPlayer(CharacterClass.Cleric, pos++);
			SpawnPlayer(CharacterClass.Rogue, pos);
		}

		void SpawnPlayer(CharacterClass characterClass, int position)
		{
			Debug.Log("Player Pos " + position);
			if (position >= WorldStateManager.Instance.CharacterStats.Count)
			{
				var newCharObject = new GameObject().AddComponent<CharacterStats>();
				newCharObject.transform.parent = WorldStateManager.Instance.transform;
				var newChar = newCharObject.GetComponent<CharacterStats>();
				newChar.SetClass(characterClass);
				//Object.DontDestroyOnLoad(newCharObject);
				WorldStateManager.Instance.CharacterStats.Add(newChar);
			}
		}

	}
}
