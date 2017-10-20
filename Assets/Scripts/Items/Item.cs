using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [Serializable]
    public class Item : ScriptableObject
    {
        public int Id;
        public int Value;
        public ItemType ItemType = ItemType.Basic;

    }

    public enum ItemType
    {
        Basic = 0,
        Weapon,
        Equipment,
        Potion,
    }
}
