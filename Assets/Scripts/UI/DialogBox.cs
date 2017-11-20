using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {

    static DialogBox _instance;
    public static DialogBox Instance  { get
        {
            if(_instance == null)
            {
                _instance = Instantiate(Resources.Load<DialogBox>("DialogBox"));
            }
            return _instance;
        }
    }

    public static bool IsOpen {get {return _instance != null;}}

    [SerializeField]
    private Text TextUI;

    private string _text;
    private int _showSize = -1;

	// Use this for initialization
	void Start () {
		TextUI.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Submit") || Input.GetMouseButtonDown(0))
        {
            if(_showSize == -1)
            {
                //done
                Destroy(gameObject);
            }
            else
            {
                FullShow();
            }
            
        }
	}

    public void ShowText(string text)
    {
        _text = text;
        _showSize = 1;
        StopAllCoroutines();
        StartCoroutine(RunShowing());
    }

    public void FullShow()
    {
        StopAllCoroutines();
        TextUI.text = _text;
        _showSize = -1;
    }

    IEnumerator RunShowing()
    {
        while(_showSize < _text.Length)
        {
            TextUI.text = _text.Substring(0, _showSize);
            yield return new WaitForSeconds(0.050f);
            _showSize++;
        }
        FullShow();
    }

}
