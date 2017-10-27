using System;
using System.Collections;
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

        public enum TurnState
        {
            Choose,
            Action,
            End
        }

        private TurnState _turnState = TurnState.Choose;

        public TurnState State {get { return _turnState; }}
        public CharacterStats Character {get { return _character; }}

        public bool IsPlayer  {get { return _character.Player; }}

        public CharacterTurn(BasicEncounterSetup encounter, CharacterStats character)
        {
            _encounter = encounter;
            _character = character;
        }

        public void Attack(int position, bool attackMonster)
        {
            _turnState = TurnState.Action;
            CharacterStats target;
            if (attackMonster)
            {
                 target =_encounter.Monsters[position];
                
            }
            else
            {
                target =_encounter.Players[position];
            }
            DealDamage(_character, target);
            //wait for animations...
            _encounter.StartCoroutine(AnimationTimer());
        }

        IEnumerator AnimationTimer()
        {
            
            yield return new WaitForSeconds(1.1f);
            _turnState = TurnState.End;
        }

        private void DealDamage(CharacterStats character, CharacterStats target)
        {
            if(target.Dead)
                return;

            //Damage  = Attack minus up to 50% of the ratio between attack and defense
            //D = A - (0.5*Max(1, Def/Atk))


            var damageRaw = character.Attack - (0.5f*Mathf.Max(1, target.Defense/character.Attack));
            var damage = Mathf.Max(1,Mathf.RoundToInt(damageRaw));
            target.CurrentHp -= (int)damage;

            Debug.Log("Dealt Damage: " + damage + "("+damageRaw+ ") to " +target.name + " dead: " + target.Dead);

            if (target.Dead)
            {
                target.CharacterVisual.Dead();
                _encounter.CleanTurnOrders();
            }

        }
    }
}
