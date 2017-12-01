using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [Serializable]
    public abstract class Item
    {
		public string itemName;
        public int id;
    }
}
