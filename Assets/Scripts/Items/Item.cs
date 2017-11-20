using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [Serializable]
    public class Item
    {
		public string itemName;
        public int id;
        public ItemType itemType;

    }

    public enum ItemType
    {
        Basic = 0,
        Consumable = 1,
        Equipment = 2
    }
}
