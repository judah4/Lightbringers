using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float maxFall = -3.49f; //sets max fall distance before respawning
    public int checkpoint = 0; //tracks respawn location
    public Text winText;

    private Vector3 ReLocate = new Vector3(0, 3, 0);

    private void Start()
    {
        winText.text = ""; //display nothing at first
        //scriptAccess = ReGround.GetComponent<CharacterController>();

    }
    // Update is called once per frame
    void Update ()
    {
        SetRezPos(); //sets checkpoint

        if (transform.position.y <= maxFall)
        {
            transform.position = ReLocate;

            //scriptAccess.isGrounded = true;
            gameObject.GetComponent<CharacterController>().isGrounded = true;
            //gameObject.GetComponent<CharacterController>().PlaySound(1); 
            print("death");
        }

    }



    public void SetRezPos()
    {
        switch (checkpoint)
        {
            case 0: 
                ReLocate = new Vector3 (0, 3, 0);
                break;
            case 1:
                ReLocate = new Vector3 (0, 3, 73);
                break;
            case 2:
                ReLocate = new Vector3(0, 3, 140);
                break;
            case 3:
                ReLocate = new Vector3(0, 3, 240);
                break;
            case 4:
                ReLocate = new Vector3(0, 10, 345);
                winText.text = "You Win!"; //display nothing at first
                break;

            /* you can have any number of case statements */
            default:
                ReLocate = new Vector3(0, 3, 0);
                break;
        }
        
    }

}





