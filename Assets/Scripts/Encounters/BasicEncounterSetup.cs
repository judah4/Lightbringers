using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Characters.MonsterTemplates;
using Assets.Scripts.Encounters.States;
using UnityEngine;
using Assets.Scripts.World;

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

        private EncounterStates _encounterState = EncounterStates.Begin;

        public EncounterStates EncounterState {get { return _encounterState; }}
        public float BeginTime = 3;

        public event Action OnTurn;
        public event Action<EncounterStates> OnEncounterState;

        public void Awake()
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

        void ChangeEncounterState(EncounterStates newState)
        {
            if (_encounterState != newState)
            {
                _encounterState = newState;
                if (OnEncounterState != null)
                {
                    OnEncounterState(_encounterState);
                }
            }
        }

        IEnumerator BeginTimer()
        {
            yield return new WaitForSeconds(BeginTime);

            ChangeEncounterState(EncounterStates.Play);
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
            if(WorldStateManager.Instance.CharactereStats.Count > position)
            {
                WorldStateManager.Instance.CharactereStats.Add(new CharacterStat());
            }
            var stats =  WorldStateManager.Instance.CharactereStats[position];

            var monstergm = new GameObject("Char");
            var monsterChar = monstergm.AddComponent<CharacterStats>();

            monsterChar.Level = stats.Level;
            monsterChar.SetClass(characterClass);
            
            monsterChar.Player = true;
            monsterChar.CharacterVisual = monstergm.AddComponent<CharacterVisual>();
            monsterChar.CharacterVisual.LoadModel((int)characterClass);
            Players.Add(monsterChar);

            monsterChar.transform.position = new Vector3(-5 +(position)*-.25f,.5f, 4+(position * -2));
            monsterChar.transform.eulerAngles = new Vector3(0, 90, 0);

            if(stats.Health > monsterChar.Hp)
            {
                stats.Health = monsterChar.Hp;
            }
            if(stats.Mana > monsterChar.Mana)
            {
                stats.Mana = monsterChar.Mana;
            }

            monsterChar.Hp = stats.Health;
            monsterChar.Mana = stats.Mana;


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
                    var monstergm = new GameObject("Mon");
                    var monsterChar = monstergm.AddComponent<CharacterStats>();
                    monsterChar.Setup(monsterData);
                    monsterChar.CharacterVisual = monstergm.AddComponent<CharacterVisual>();
                    monsterChar.CharacterVisual.LoadModel(monsterData.id);
                    Monsters.Add(monsterChar);

                    monsterChar.transform.position = new Vector3(5 +(cnt+monsCnt)*.25f + laneOffset,.5f, 3 + (cnt+monsCnt)*-2);
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

            if (OnTurn != null)
            {
                OnTurn();
            }

        }

        public void NextTurn()
        {
            TurnOrders.RemoveAt(0);

            if (AllDead(Players))
            {
                Debug.Log("Monsters win!");
                ChangeEncounterState(EncounterStates.GameOver);
            }
            else if (AllDead(Monsters))
            {
                Debug.Log("Players win!");
                ChangeEncounterState(EncounterStates.Results);
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

        public void Attack(int posIndex)
        {
            if(CharacterTurn == null)
                return;

            if (CharacterTurn.IsPlayer && CharacterTurn.State == CharacterTurn.TurnState.Choose)
                {
                    Debug.Log("Player: " + CharacterTurn.Character.name + " attacking position: " + posIndex);
                    CharacterTurn.Attack(posIndex, true);
                }
        }
    }
}
