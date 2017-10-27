using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBuilding : MonoBehaviour {

    public SceneChange scene;

    private void OnTriggerStay(Collider other)
    {
        //show somethong on screen to let charactr know that they can press to enter

        if (Input.GetKeyDown("F") || Input.GetButtonDown("Jump"))
        {
            //save state
            //change scene

        }
    }
}
