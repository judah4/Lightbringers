using Assets.Scripts.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class LoadCharacterStats : MonoBehaviour {
	
	public List<GameObject> CharacterPanels;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 4; i++)
		{
			var charController = (CharacterUIController)CharacterPanels[i].GetComponent(typeof(CharacterUIController));
			charController.charInfo = WorldStateManager.Instance.CharacterStats[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene(WorldStateManager.Instance.CurrentScene);
		}
	}
}
