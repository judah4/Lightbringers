using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
        {
            var chControl = other.gameObject.GetComponent<CharacterController>();
            if (WorldStateManager.Instance.Events.Contains(500))
            {
                
                SceneManager.LoadScene("WinScene");
            }
        }
}
