using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPVisual : MonoBehaviour {

    public List<CharacterStats> Monsters = new List<CharacterStats>();
    public Text experience;

    // Use this for initialization
    void Start () {
        experience.text = "Experience: ";
	}
	
}
