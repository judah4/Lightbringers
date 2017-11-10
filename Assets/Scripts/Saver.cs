using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Saver : MonoBehaviour
{
	CharacterStats testData;
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.OpenOrCreate);

		testData = new CharacterStats();

		bf.Serialize(file, testData);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/saveData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);

			testData = (CharacterStats)bf.Deserialize(file);
		}
	}
}
