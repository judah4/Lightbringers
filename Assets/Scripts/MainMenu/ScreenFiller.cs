using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var quadHeight = Camera.main.orthographicSize * 2.0;
		var quadWidth = quadHeight * Screen.width / Screen.height;
		transform.localScale = new Vector3((float)quadWidth, (float)quadHeight, 1);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
