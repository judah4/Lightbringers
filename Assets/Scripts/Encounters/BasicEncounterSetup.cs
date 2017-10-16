using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Characters.MonsterTemplates;
using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public class BasicEncounterSetup : MonoBehaviour
    {
        public int EncounterId;
        public List<CharacterStats> Monsters = new List<CharacterStats>();

        public List<CharacterStats> Players = new List<CharacterStats>();

        public List<CharacterStats> TurnOrders = new List<CharacterStats>();
        public TurnOrder TurnOrder { get; set; }
        public void Start()
        {
            SpawnPlayers();
            SpawnEncounter();
            
            //order
            TurnOrder = new TurnOrder(this);
            TurnOrder.Generate();

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
    }
}
