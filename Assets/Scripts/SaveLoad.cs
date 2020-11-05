﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public static class SaveLoad 
{

	public static void SaveChallenges(ChallengeMenu challenges){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/challenges.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		ChallengeData data = new ChallengeData(challenges);
		formatter.Serialize(stream, data);
		stream.Close();
	} 
	public static void LoadChallenges(ChallengeMenu challenges){
		string path = Application.persistentDataPath + "/challenges.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			ChallengeData data =formatter.Deserialize(stream) as ChallengeData;
			stream.Close();
			challenges.challengeList = data.challengeList;
			
		} else {
			Debug.LogError("No item save file at "+ path );
		}
	}

	public static void SaveInventory(Inventory inventory){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/inventory.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		InventoryData data = new InventoryData(inventory);
		formatter.Serialize(stream,data);
		stream.Close();
	}

	public static void LoadInventory(Inventory inventory){
		string path = Application.persistentDataPath + "/inventory.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			InventoryData data =formatter.Deserialize(stream) as InventoryData;
			stream.Close();
			inventory.occupied = data.isFull;
			inventory.quantity = data.quantity;
			inventory.itemDataArr = data.itemDataArr;
			
		} else {
			Debug.LogError("No item save file at "+ path );
		}
	}
	public static void SaveItemsOnFloor(ItemsOnFloorList itemsOnFloorList)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/itemsOnFloor.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		ItemsOnFloorData data = new ItemsOnFloorData(itemsOnFloorList);
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static void LoadItemsOnFloor(ItemsOnFloorList items)
	{
		string path = Application.persistentDataPath + "/itemsOnFloor.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			ItemsOnFloorData data =formatter.Deserialize(stream) as ItemsOnFloorData;
			stream.Close();
			items.itemDataList = data.itemDataList;
			
		} else {
			Debug.LogError("No item save file at "+ path );
		}
	} 

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