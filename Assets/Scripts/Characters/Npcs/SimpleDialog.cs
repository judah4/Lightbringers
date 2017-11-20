using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialog : MonoBehaviour {

    [TextArea]
    public string text;

    private bool _inRange = false; 
    private float timer = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(DialogBox.IsOpen || !_inRange)
        {
            
            return;
        }

        timer--;
        if(timer < 0)
        {
            _inRange = false;
        }


		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Submit"))
        {
            DialogBox.Instance.ShowText(text);
        }
	}

    void OnTriggerStay(Collider other)
    {
        if(DialogBox.IsOpen)
        {
            return;
        }

        var chControl = other.gameObject.GetComponent<CharacterController>();
        if (chControl != null)
        {
            _inRange = true;
            timer = 0.5f;
            ContextBox.Instance.ShowText("Talk");
        }
    }

    void OnTriggerExit(Collider other)
    {
        var chControl = other.gameObject.GetComponent<CharacterController>();
        if (chControl != null)
        {
            _inRange = false;
            ContextBox.Instance.Hide();
        }
    }

}
