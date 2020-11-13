 using UnityEngine;
 
 [System.Serializable]
 public class RockData
 {
	  public float[] position;

	  public RockData(GameObject Rock){
		position = new float[2];
        position[0] = Rock.transform.position.x;
        position[1] = Rock.transform.position.y;
	  }
 }