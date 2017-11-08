using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.World
{
    [Serializable]
    public class CharacterStat
    {
        public int Level = 1;
        public int Exp = 0;
        public int Health = 9999;
        public int Mana = 9999;
    }
}
