
using UnityEngine;


 [System.Serializable]
public class MapSeedData
{
	public int seed;

	public MapSeedData(ProcGenDungeon map){
		seed = map.seed;
	}
	

}