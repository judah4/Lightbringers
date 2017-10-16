using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Encounters
{
    [Serializable]
    public class EncounterData
    {
        public EncounterMonsterData monsters;
    }


    [Serializable]
    public class EncounterMonsterData
    {
        public List<MonsterLaneData> frontLane;
       public List<MonsterLaneData> backLane;
    }
     [Serializable]
    public class MonsterLaneData
    {
        public int id;
        public int count;
    }
}
