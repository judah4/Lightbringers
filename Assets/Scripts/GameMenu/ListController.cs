using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour {

	public InventoryManager Inventory;
	public GameObject ItemUiPrefab;
	public GameObject ContentPanel;

	void Start()
	{
		UpdateInventoryUI();
	}

	private void UpdateInventoryUI()
	{
		foreach(Item item in Inventory.items)
		{
			GameObject itemPanel = Instantiate(ItemUiPrefab) as GameObject;
			ItemController controller = itemPanel.GetComponent<ItemController>();

			controller.itemName.text = item.itemName;

			//itemPanel.transform.parent = ContentPanel.transform;
			itemPanel.transform.SetParent(ContentPanel.transform);
		}
	}
}
