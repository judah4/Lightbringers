using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assets.Scripts.Encounters;
using UnityEngine;

namespace Assets.Scripts.Characters.MonsterTemplates
{
    public static class MonsterLoader
    {
        public static MonsterData LoadEncounterData(int monsterId)
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Data/Monsters/Monster"+monsterId+".json");
            var result = System.IO.File.ReadAllText(filePath);
            var encounter = JsonUtility.FromJson<MonsterData>(result);
            return encounter;
        }
    }
}
