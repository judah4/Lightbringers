using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Encounters;
using UnityEngine;

public class MusicForEncounter : MonoBehaviour
{

    public List<AudioClip> clips;
    public AudioSource Source;

    public BasicEncounterSetup EncounterSetup;

	// Use this for initialization
	void Start () {
	    if (EncounterSetup.EncounterId == 13)
	    {
	        Source.clip = clips[0];
            Source.Play();
	    }
        else if (EncounterSetup.EncounterId == 14)
	    {
	        Source.clip = clips[1];
            Source.Play();
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
