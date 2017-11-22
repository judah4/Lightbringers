using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour {

	public CharacterStats charInfo;
	public GameObject namePanel, statPanel;
	// Use this for initialization
	void Start () {
		UpdateUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateUI()
	{
		SetField(namePanel, "Name", charInfo.name);
		SetField(namePanel, "Class", charInfo.CharacterClass);
		SetField(namePanel, "Level", charInfo.Level);

		SetField(namePanel, "Experience", charInfo.Exp + "/" + charInfo.ExpNeeded);
		SetField(namePanel, "Hp", charInfo.CurrentHp + "/" + charInfo.Hp);
		SetField(namePanel, "Mana", charInfo.CurrentMana + "/" + charInfo.Mana);

		SetField(statPanel, "Attack", charInfo.Attack);
		SetField(statPanel, "Defence", charInfo.Defense);
		SetField(statPanel, "Speed", charInfo.Speed);
	}

	private void SetField(GameObject panel, string fieldName, object input)
	{
		Transform fieldTransform = panel.transform.Find(fieldName);
		Text fieldText = fieldTransform.GetComponent<Text>();
		fieldText.text = fieldName + ": " + input;

		if(fieldName == "Name")
		{
			fieldText.text = input.ToString();
		}
	}

	private void SetImage()
	{

	}
}
