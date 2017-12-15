using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTree : MonoBehaviour {

    public List<DialogBranch> Branches = new List<DialogBranch>();

}

[Serializable]
public class DialogBranch
{

    public int Id = 0;
    public bool CompleteEvent;
    public int EncounterId=0;
    public int FightId = 0;
    public List<TextAndAudio> Texts;
}

[Serializable]
public class TextAndAudio
{
    public string Text;
    public AudioClip Audio;
}
