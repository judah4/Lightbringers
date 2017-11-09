using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TransitionScript : MonoBehaviour
{

    public Vector3 LoadLocation; //holds the position the player will spawn in the next location
    public string SceneName;



    void OnTriggerEnter(Collider other) //while player model is inside the object
    {
        //checks to see that it was the player that is in transition area and not something else
        if (other.CompareTag("Player")) 
        {
            print("inside change location");


            //insert code here to show UI thing to prompt user to interact
            //interactionUIThing.enabled = true;

            if (Input.GetButton("Submit"))
            {
                SceneManager.LoadScene(SceneName);

                print(("tried to switch to " + SceneName));
            }
        }
        
    }

    void OnTriggerExit(Collider other) //while player model is leaves the object
    {
        if (other.CompareTag("Player"))
        {
            print("left change location");
            //hide UI thing for User to interact
            //interactionUIThing.enabled = false;

        }
    }

}
