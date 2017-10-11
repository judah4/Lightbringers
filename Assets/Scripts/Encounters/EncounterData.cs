using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Encounters
{
    [Serializable]
    public class EncounterData
    {
        public List<EncounterMonsterData> monsters;
    }

    [Serializable]
    public class EncounterMonsterData
    {
        public int id;
        public int count;
    }
}
