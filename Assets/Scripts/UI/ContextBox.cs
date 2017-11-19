using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextBox : MonoBehaviour {

	static ContextBox _instance;
    public static ContextBox Instance  { get
        {
            if(_instance == null)
            {
                _instance = Instantiate(Resources.Load<ContextBox>("ContextBox"));
            }
            return _instance;
        }
    }

    public static bool IsOpen {get {return _instance != null;}}

    [SerializeField]
    private Text _textUI;

    private float timer = 0.5f;

	// Use this for initialization
	void Start ()
    {
		_textUI.text = "";
	}

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void ShowText(string text)
    {
        _textUI.text = text;
        timer = 0.5f;
    }

}
