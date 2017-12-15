using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Encounters;
using Assets.Scripts.World;
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

    public DialogTree DialogTree;
    public int dialogId = 0;
    public int dialogIndex = 0;
    public int dialogTextIndex = 0;
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
                if (DialogTree != null)
                {
                    dialogTextIndex++;
                    if (dialogTextIndex < DialogTree.Branches[dialogIndex].Texts.Count)
                    {
                        ShowText(DialogTree.Branches[dialogIndex].Texts[dialogTextIndex]);
                        return;
                    }
                }
                //done
                Destroy(gameObject);
            }
            else
            {
                FullShow();
            }
            
        }
	}

    public void ShowText(TextAndAudio text)
    {
        _text = text.Text;

        if (text.Audio != null)
        {
            //play
            AudioSource.PlayClipAtPoint(text.Audio, Camera.main.transform.position);
        }

        _showSize = 1;
        StopAllCoroutines();
        StartCoroutine(RunShowing());
    }

    public void ShowText(DialogTree dialogTree)
    {
        DialogTree = dialogTree;

        for (int cnt = 0; cnt < DialogTree.Branches.Count; cnt++)
        {
            if (WorldStateManager.Instance.Events.Contains(DialogTree.Branches[cnt].Id))
            {
                continue;
            }

            dialogId = DialogTree.Branches[cnt].Id;
            dialogIndex = cnt;
            break;
        }

        if (DialogTree.Branches[dialogIndex].CompleteEvent)
        {
            WorldStateManager.Instance.Events.Add(DialogTree.Branches[dialogIndex].Id);
        }

        dialogTextIndex = 0;
        ShowText(DialogTree.Branches[dialogIndex].Texts[dialogTextIndex]);

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
