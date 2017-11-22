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
		int count = 0;
		foreach(Item item in Inventory.items)
		{
			GameObject itemPanel = Instantiate(ItemUiPrefab) as GameObject;
			ItemController controller = itemPanel.GetComponent<ItemController>();

			controller.itemName.text = item.itemName;

			itemPanel.transform.SetParent(ContentPanel.transform);
			count++;
		}

		if (count < 25)
		{
			for(int i = count; i < 25; i++)
			{
				GameObject itemPanel = Instantiate(ItemUiPrefab) as GameObject;
				ItemController controller = itemPanel.GetComponent<ItemController>();

				controller.itemName.text = "";

				itemPanel.transform.SetParent(ContentPanel.transform);
			}
		}
	}
}
