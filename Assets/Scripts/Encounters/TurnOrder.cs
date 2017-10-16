using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Encounters
{
    public class TurnOrder
    {
        public List<CharacterStats> Characters;
        public int Tick = 1;
        public BasicEncounterSetup Encounter;

        public TurnOrder(BasicEncounterSetup encounter)
        {
            Encounter = encounter;
            Characters = encounter.TurnOrders;
        }

        public void Generate()
        {
            Characters.Clear();

            var tickPlus = 0;
            while (Characters.Count < 10)
            {
                CheckOrder(Tick+tickPlus, Encounter.Players);
                CheckOrder(Tick+tickPlus, Encounter.Monsters);

                if (Characters.Count < 1)
                {
                    //everyone is dead?
                    break;
                }
                tickPlus++;
            }

        }

        void CheckOrder(int tick, List<CharacterStats> characters )
        {
            for (int cnt = 0; cnt < characters.Count; cnt++)
            {
                
                var character = characters[cnt];
                if(character.Dead)
                    continue;

                Characters.Add(character);
                
            }
        }

    }
}
