using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Characters.MonsterTemplates;
using Assets.Scripts.Encounters.States;
using UnityEngine;
using Assets.Scripts.World;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

        public GameObject GameOverPanel;
        public GameObject victoryPanel;
        public Text sumExperience;
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
        public int totalExp = 0;

        public void Awake()
        {
            EncounterId = WorldStateManager.Instance.EncounterId;
            SpawnPlayers();
            SpawnEncounter();
            for (int cnt = 0; cnt < Monsters.Count; cnt++)
            {
                if(Monsters[cnt] != null)
                    totalExp += Monsters[cnt].Exp;
            }

            sumExperience.text = "Experience: " + Convert.ToString(totalExp);

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

            OnEncounterState += (state) =>
            {
                if(state != EncounterStates.Results)
                    return;

                //change music

                //everyone victory!
                for(int cnt = 0; cnt < Players.Count; cnt++)
                {
                     Players[cnt].CharacterVisual.Character.Trigger(AnimationTrigger.Victory);
                }


                for(int cnt = 0; cnt < Players.Count; cnt++)
                {
                     Players[cnt].GiveExp(totalExp);
                }
                victoryPanel.SetActive(true);
            };
        }

        void SpawnPlayer(CharacterClass characterClass, int position)
        {
            Debug.Log("Player Pos " + position);
            if(position >= WorldStateManager.Instance.CharacterStats.Count)
            {
                var charStat = new GameObject("char").AddComponent<CharacterStats>();
                WorldStateManager.Instance.CharacterStats.Add(charStat);
                charStat.SetClass(characterClass);
            }
            var stats =  WorldStateManager.Instance.CharacterStats[position];

            var monstergm = new GameObject("Char");
            var monsterChar = monstergm.AddComponent<CharacterStats>();

            monsterChar.Level = stats.Level;
            monsterChar.SetClass(characterClass);
            
            monsterChar.Player = true;
            monsterChar.CharacterVisual = monstergm.AddComponent<CharacterVisual>();
            monsterChar.CharacterVisual.LoadModel((int)characterClass);
            Players.Add(monsterChar);

            monsterChar.transform.position = new Vector3(-8 +(position*-.25f),.5f, 8+(position * -4));
            monsterChar.transform.eulerAngles = new Vector3(0, 90, 0);

            if(stats.CurrentHp > monsterChar.MaxHp)
            {
                stats.CurrentHp = monsterChar.MaxHp;
            }
            if(stats.MaxMana > monsterChar.MaxMana)
            {
                stats.MaxMana = monsterChar.MaxMana;
            }

            monsterChar.CurrentHp = stats.CurrentHp;
            monsterChar.CurrentMana = stats.MaxMana;
            monsterChar.Exp = stats.Exp;

            monsterChar.OnHealthChange += (health) => {
                stats.CurrentHp = health;
            };
             monsterChar.OnLevel += (level) => {
                stats.Level = level;
            };
             monsterChar.OnExp += (exp, earned) => {
                stats.Exp = exp;
            };

        }

        void SpawnEncounter()
        {
            var encounter = EncounterLoader.LoadEncounterData(EncounterId);
            LoadLane(encounter.monsters.frontLane, 0);
            LoadLane(encounter.monsters.backLane, 5);

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

                    monsterChar.transform.position = new Vector3(8 +(cnt+monsCnt)*.25f + laneOffset,.5f, 6 + (cnt+monsCnt)*-4);
                    monsterChar.transform.eulerAngles = new Vector3(0, -90, 0);

                }

                

            }

            while (Monsters.Count % 3 != 0)
            {
                Monsters.Add(null);
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
                    throw new System.Exception("Games over man");
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
                GameOverPanel.SetActive(true);
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
                if(characters[cnt] == null)
                    continue;

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
            var liveList = new List<int>();
            for (int cnt = 0; cnt < characters.Count; cnt++)
            {
                if(characters[cnt] == null)
                    continue;

                var mon = characters[cnt];
                if (mon.Dead == false)
                {
                    liveList.Add(cnt);
                    //return cnt;
                }
            }
            if(liveList.Count < 1)
                return 0;

            return liveList[Random.Range(0, liveList.Count)];
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

        public void MoveOffenseAction(int posIndex)
        {
            if (CharacterTurn == null)
                return;

            if (CharacterTurn.IsPlayer && CharacterTurn.State == CharacterTurn.TurnState.Choose)
            {
                Debug.Log("Player: " + CharacterTurn.Character.name + " attacking position: " + posIndex);
                CharacterTurn.Attack(posIndex, true);
            }
        }

        public void OffenseItem(int posIndex, int itemId)
        {
            if (CharacterTurn == null)
                return;

            if (CharacterTurn.IsPlayer && CharacterTurn.State == CharacterTurn.TurnState.Choose)
            {
                Debug.Log("Player: " + CharacterTurn.Character.name + " attacking position: " + posIndex);
                CharacterTurn.Attack(posIndex, true);
            }
        }
    }
}
