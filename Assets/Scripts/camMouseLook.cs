using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour {
    //grabs hold of the movememnt of the mouse


    Vector2 mouseLook; //keeps track of how much moevement has occured
    Vector2 smoothV; //smooths moevements to the frame
    public float sensitivity = 4.0f; //sensitivity amount
    public float smoothing = 2.0f; //smoothing multiple

    private float XAxis;
    private float YAxis;

    GameObject character; 

	// Use this for initialization
	void Start () {
        //character = this.transform.parent.gameObject; //locks to character
        
    }
	
	// Update is called once per frame
	void Update () {

       
       /*
        var md = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxisRaw("Mouse Y")); //gets the mouse moevements
        


        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing); //applies smoothing to x-axis
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing); //applies smoothing applies smoothing to y axis
        mouseLook += smoothV; //
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f); //locks maximum rotation amount up and down

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right); //up and down (-mouseLook to invert)
        //character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up); //left and right
        */
    }
}
