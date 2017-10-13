using UnityEngine;

namespace Assets.Scripts.Encounters
{
    public static class EncounterLoader {

        public static EncounterData LoadEncounterData(int encounterId)
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "Data/Encounters/Encounter"+encounterId+".json");
            var result = System.IO.File.ReadAllText(filePath);
            var encounter = JsonUtility.FromJson<EncounterData>(result);
            return encounter;
        }
    }
}
