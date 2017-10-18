using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Encounters.States
{
    public class CharacterTurn
    {
        private BasicEncounterSetup _encounter;
        private CharacterStats _character;

        public CharacterStats Character {get { return _character; }}

        public bool IsPlayer  {get {return _character.Player == false;}}
        public bool Waiting {get { return IsPlayer; }}

        public CharacterTurn(BasicEncounterSetup encounter, CharacterStats character)
        {
            _encounter = encounter;
            _character = character;
        }

        public void Attack(int position, bool attackMonster)
        {
            if (attackMonster)
            {
                 var target =_encounter.Monsters[position];
                DealDamage(_character, target);
            }
            //wait for animations...
            _encounter.NextTurn();
        }

        private void DealDamage(CharacterStats character, CharacterStats target)
        {
            if(target.Dead)
                return;

            //Damage  = Attack minus up to 50% of the ratio between attack and defense
            //D = A - (0.5*Max(1, Def/Atk))


            var damage = character.Attack - (0.5*Mathf.Max(1, target.Defense/character.Attack));
            target.CurrentHp -= (int)damage;

            Debug.Log("Dealt Damage: " + damage + " to " +target.name + " dead: " + target.Dead);

        }
    }
}
