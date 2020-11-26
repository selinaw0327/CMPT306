
using UnityEngine;


 [System.Serializable]
public class MapSeedData
{
	public int seed;
	public float[] exitPosition;

	public MapSeedData(ProcGenDungeon map){
		seed = map.seed;
		exitPosition = map.exitPosition;
	}
	

}