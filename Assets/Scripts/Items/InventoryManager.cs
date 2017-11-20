using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class InventoryManager : MonoBehaviour
    {
        private static InventoryManager _instance;

        public static InventoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("Inventory").AddComponent<InventoryManager>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        public List<Item> items = new List<Item>();

	}
}
