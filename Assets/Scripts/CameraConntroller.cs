using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConntroller : MonoBehaviour {

    public GameObject player;
    private Vector3 offset; //current trans pos of camera - current trans pos of player obj

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
        //as we move our player with the controls, camera is moved each frame to allign with player obj
	}//runs after all items are run in update
}
