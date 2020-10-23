using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SaveLoad 
{

	public static void SavePlayer (PlayerStats player) 
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.info";
		FileStream stream = new FileStream(path, FileMode.Create);
		PlayerData data = new PlayerData(player);
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static void LoadPlayer (PlayerStats player)
	{
		string path = Application.persistentDataPath + "/player.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			PlayerData data =formatter.Deserialize(stream) as PlayerData;
			stream.Close();
			
			player.currentLevel = data.Level;
            player.currentHealth = data.Health;
            player.currentEnergy = data.Energy;
            player.currentHunger = data.Hunger;
			player.damage = data.Damage;
			
            
			Vector2 position;
			position.x = data.position[0];
			position.y = data.position[1];
			player.transform.position = position;
			

		} else {
			Debug.LogError("No player save file at "+ path );
			
		}
	}
}