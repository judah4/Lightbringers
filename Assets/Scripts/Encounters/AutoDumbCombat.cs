﻿using System.Collections.Generic;
using Assets.Scripts.Encounters.States;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class AutoDumbCombat : MonoBehaviour
    {
        [SerializeField]
        private BasicEncounterSetup Encounter;

        // Use this for initialization
        void Start () {
            if (Encounter == null)
            {
                Encounter = GetComponent<BasicEncounterSetup>();
            }
        }
	
        // Update is called once per frame
        void Update () {
            if (Encounter.EncounterState == BasicEncounterSetup.EncounterStates.Play)
            {
                if (Encounter.CharacterTurn.IsPlayer && Encounter.CharacterTurn.State == CharacterTurn.TurnState.Choose)
                {
                    var pos = Encounter.PickTargetPosition(true);
                    Debug.Log("Player: " + Encounter.CharacterTurn.Character.name + " attacking position: " + pos);
                    Encounter.CharacterTurn.Attack(pos, true);
                }
            }
        }

        
    }
}
