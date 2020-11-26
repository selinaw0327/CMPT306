using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public static class SaveLoad 
{
	public static void SaveMenuInfo(){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/menu.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		MenuData data = new MenuData();
		formatter.Serialize(stream, data);
		stream.Close();

	}

	public static MenuData LoadMenuInfo(){
		string path = Application.persistentDataPath + "/menu.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			MenuData data =formatter.Deserialize(stream) as MenuData;
			stream.Close();
			return data;
			
		} else {
			Debug.LogError("No item save file at "+ path );
			return null;
		}
	}
	
	public static void SaveMapSeed(ProcGenDungeon map){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/seed.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		MapSeedData data = new MapSeedData(map);
		formatter.Serialize(stream, data);
		stream.Close();
	}
	public static void LoadMapSeed(ProcGenDungeon map){
		string path = Application.persistentDataPath + "/seed.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			MapSeedData data =formatter.Deserialize(stream) as MapSeedData;
			stream.Close();
			map.seed = data.seed;
			map.exitPosition = data.exitPosition;
			
		} else {
			Debug.LogError("No item save file at "+ path );
		}
	}
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
			inventory.items = data.items;
			
		} else {
			Debug.LogError("No item save file at "+ path );
		}
	}

	public static void SaveEnemies(EnemyLists enemyLists){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/enemies.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		enemyLists.batDataList.Clear();
		enemyLists.wormDataList.Clear();
		enemyLists.ratDataList.Clear();
		enemyLists.skelDataList.Clear();
		enemyLists.vampDataList.Clear();
		enemyLists.zombDataList.Clear();
		foreach(GameObject bat in enemyLists.batList){
			if(bat != null){
				EnemyData newData = new EnemyData(bat.GetComponentInChildren<EnemyStats>());
				enemyLists.batDataList.Add(newData);
			}
		}
		foreach(GameObject worm in enemyLists.wormList){
			if(worm != null){
				EnemyData newData = new EnemyData(worm.GetComponentInChildren<EnemyStats>());
				enemyLists.wormDataList.Add(newData);
			}
		}
		foreach(GameObject rat in enemyLists.ratList){
			if(rat != null){
				EnemyData newData = new EnemyData(rat.GetComponentInChildren<EnemyStats>());
				enemyLists.ratDataList.Add(newData);
			}
		}
		foreach(GameObject skel in enemyLists.skelList){
			if(skel != null){
				EnemyData newData = new EnemyData(skel.GetComponentInChildren<EnemyStats>());
				enemyLists.skelDataList.Add(newData);
			}
		}
		foreach(GameObject vamp in enemyLists.vampList){
			if(vamp != null){
				EnemyData newData = new EnemyData(vamp.GetComponentInChildren<EnemyStats>());
				enemyLists.vampDataList.Add(newData);
			}
		}
		foreach(GameObject zomb in enemyLists.zombList){
			if(zomb != null){
				EnemyData newData = new EnemyData(zomb.GetComponentInChildren<EnemyStats>());
				enemyLists.zombDataList.Add(newData);
			}
		}
		EnemyDataLists data = new EnemyDataLists(enemyLists);
		
	
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static void LoadEnemies(EnemyLists enemyLists){
		string path = Application.persistentDataPath + "/enemies.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			EnemyDataLists data =formatter.Deserialize(stream) as EnemyDataLists;
			stream.Close();
			enemyLists.batDataList = data.batDataList;
			enemyLists.ratDataList = data.ratDataList;
			enemyLists.wormDataList = data.wormDataList;
			enemyLists.vampDataList = data.vampDataList;
			enemyLists.skelDataList = data.skelDataList;
			enemyLists.zombDataList = data.zombDataList;
			
		} else {
			Debug.LogError("No item save file at "+ path );
		}
	}
	public static void SaveRocks(RockList rockList){
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/rocks.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		rockList.rockDataList.Clear();
		rockList.smallRockOneDataList.Clear();
		rockList.smallRockTwoDataList.Clear();
		foreach(GameObject rock in rockList.rockList){
			if(rock != null){
				RockData newData = new RockData(rock);
				rockList.rockDataList.Add(newData);
			}
		}
		foreach(GameObject rock2 in rockList.smallRockOneList){
			if(rock2 != null){
				RockData newData = new RockData(rock2);
				rockList.smallRockOneDataList.Add(newData);
			}
		}
		foreach(GameObject rock3 in rockList.smallrockTwoList){
			if(rock3 != null){
				RockData newData = new RockData(rock3);
				rockList.smallRockTwoDataList.Add(newData);
			}
		}
		RockDataList data = new RockDataList(rockList);
		formatter.Serialize(stream, data);
		stream.Close();
	}
	public static void LoadRocks(RockList rockList)
	{
		string path = Application.persistentDataPath + "/rocks.info";
		if(File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			RockDataList data =formatter.Deserialize(stream) as RockDataList;
			stream.Close();
			rockList.rockDataList = data.rockDataList;
			rockList.smallRockTwoDataList = data.smallRockTwoDataList;
			rockList.smallRockOneDataList = data.smallRockOneDataList;
			
		} else {
			Debug.LogError("No item save file at "+ path );
		}
	}
	public static void SaveItemsOnFloor(ItemsOnFloorList itemsOnFloorList)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/itemsOnFloor.info";
		FileStream stream  =  new FileStream(path, FileMode.Create);
		itemsOnFloorList.itemDataList.Clear();
		foreach(GameObject item in itemsOnFloorList.itemList){
			if(item != null){
				ItemData newData = new ItemData(item.GetComponent<Item>());
				itemsOnFloorList.itemDataList.Add(newData);
			}
		}
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
			
			
            player.currentHealth = data.Health;
            player.currentEnergy = data.Energy;
            player.currentHunger = data.Hunger;
			player.overallDamage = data.Damage;
			player.overallHealth = data.overallHealth;
			player.maxHunger = data.maxhunger;
			player.maxEnergy = data.maxenergy;
			player.swordEquipped =  data.swordEquipped;
			player.sword = data.sword;
			
			
            
			Vector2 position;
			position.x = data.position[0];
			position.y = data.position[1];
			player.transform.position = position;
			

		} else {
			Debug.LogError("No player save file at "+ path );
			
		}
	}



}