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
    public List<string> Texts;
}
