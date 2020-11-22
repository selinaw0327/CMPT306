using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    public float DestroyTime = 3f;
    public Vector3 offset = new Vector3(0, 1, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        // Destroy(gameObject, DestroyTime);

        transform.localPosition += offset;
        
        // transform.localPosition += new Vector3(Random.Range(RandomizeIntensity.x, RandomizeIntensity.x),
        // Random.Range(RandomizeIntensity.y, RandomizeIntensity.y),
        // Random.Range(RandomizeIntensity.z, RandomizeIntensity.z));

        
    }

   
}
