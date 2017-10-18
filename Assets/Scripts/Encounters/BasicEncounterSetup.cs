using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Characters.MonsterTemplates;
using Assets.Scripts.Encounters.States;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class BasicEncounterSetup : MonoBehaviour
    {
        public enum EncounterStates
        {
            Begin,
            Play,
            Results,
            GameOver
        }

        public int EncounterId;
        public List<CharacterStats> Monsters = new List<CharacterStats>();

        public List<CharacterStats> Players = new List<CharacterStats>();

        public List<CharacterStats> TurnOrders = new List<CharacterStats>();
        public TurnOrder TurnOrder { get; set; }

        public CharacterTurn CharacterTurn;

        public EncounterStates EncounterState = EncounterStates.Begin;
        public float BeginTime = 3;

        public void Start()
        {
            SpawnPlayers();
            SpawnEncounter();
            
            //order
            TurnOrder = new TurnOrder(this);
            TurnOrder.Generate();

            StartCoroutine(BeginTimer());
            //StartTurn();
            

        }

        void Update()
        {
            
            if (EncounterState == EncounterStates.Play)
            {
                if (CharacterTurn.IsPlayer == false)
                {
                    if (CharacterTurn.State == CharacterTurn.TurnState.Choose)
                    {
                        bool attackMonster = false;
                        var pos = PickTargetPosition(attackMonster);
                        Debug.Log("Monster: " + CharacterTurn.Character.name + " attacking position: " + pos);
                        CharacterTurn.Attack(pos, attackMonster);
                    }
                }

                if (CharacterTurn.State == CharacterTurn.TurnState.End)
                {
                    NextTurn();
                }
            }
        }

        IEnumerator BeginTimer()
        {
            yield return new WaitForSeconds(BeginTime);

            EncounterState = EncounterStates.Play;
            StartTurn();
        }


        void SpawnPlayers()
        {
            var pos = 0;
            SpawnPlayer(CharacterClass.Warrior, pos++);
            SpawnPlayer(CharacterClass.Wizard, pos++);
            SpawnPlayer(CharacterClass.Cleric, pos++);
            SpawnPlayer(CharacterClass.Rogue, pos);
        }

        void SpawnPlayer(CharacterClass characterClass, int position)
        {
            var monstergm = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var monsterChar = monstergm.AddComponent<CharacterStats>();
            monsterChar.SetClass(characterClass);
            monsterChar.Player = true;
            Players.Add(monsterChar);

            monsterChar.transform.position = new Vector3(-5 +(position)*-.25f,.5f, (position)*2);
            monsterChar.transform.eulerAngles = new Vector3(0, 90, 0);
        }

        void SpawnEncounter()
        {
            var encounter = EncounterLoader.LoadEncounterData(EncounterId);
            LoadLane(encounter.monsters.frontLane, 0);
            LoadLane(encounter.monsters.backLane, 2);

        }

        void LoadLane(List<MonsterLaneData> laneData, float laneOffset)
        {
            for (int cnt = 0; cnt < laneData.Count; cnt++)
            {
                var encounterMonster = laneData[cnt];
                for (int monsCnt = 0; monsCnt < encounterMonster.count; monsCnt++)
                {
                    //spawn
                    var monsterData = MonsterLoader.LoadEncounterData(encounterMonster.id);
                    var monstergm = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    var monsterChar = monstergm.AddComponent<CharacterStats>();
                    monsterChar.Setup(monsterData);
                    Monsters.Add(monsterChar);

                    monsterChar.transform.position = new Vector3(5 +(cnt+monsCnt)*.25f + laneOffset,.5f, (cnt+monsCnt)*2);
                    monsterChar.transform.eulerAngles = new Vector3(0, -90, 0);

                }
            }
        }

        void StartTurn()
        {
            if (TurnOrders.Count < 1)
            {
                TurnOrder.Generate();
                if (TurnOrders.Count < 1)
                {
                    //everyone dead?
                    throw new Exception("Games over man");
                }

            }
            var charTurn = TurnOrders[0];
            CharacterTurn = new CharacterTurn(this, charTurn);

        }

        public void NextTurn()
        {
            TurnOrders.RemoveAt(0);

            if (AllDead(Players))
            {
                Debug.Log("Monsters win!");
                EncounterState = EncounterStates.GameOver;
            }
            else if (AllDead(Monsters))
            {
                Debug.Log("Players win!");
                EncounterState = EncounterStates.Results;
            }
            else
            {
                StartTurn();
            }

        }

        bool AllDead(List<CharacterStats> characters)
        {
            for (int cnt = 0; cnt < characters.Count; cnt++)
            {
                if (characters[cnt].Dead == false)
                    return false;
            }
            return true;
        }

        public int PickTargetPosition(bool attackMonster)
        {
            if (attackMonster)
            {
                return PickTargetPositionFromList(Monsters);
            }
            else
            {
                return PickTargetPositionFromList(Players);
            }

        }

        int PickTargetPositionFromList(List<CharacterStats> characters )
        {

            for (int cnt = 0; cnt < characters.Count; cnt++)
            {
                var mon = characters[cnt];
                if (mon.Dead == false)
                {
                    return cnt;
                }
            }
            return 0;
        }

        public void CleanTurnOrders()
        {
            int cnt = 0;
            while (cnt < TurnOrders.Count)
            {
                if (TurnOrders[cnt].Dead)
                {
                    TurnOrders.RemoveAt(cnt);
                    continue;
                }
                cnt++;
            }


        }
    }
}
