using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialog : MonoBehaviour {

    [TextArea]
    public string text;

    private bool _inRange = false; 

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

		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E))
        {
            DialogBox.Instance.ShowText(text);
        }
        _inRange = false;
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
            ContextBox.Instance.ShowText("Talk");
        }
    }

}
