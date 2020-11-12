using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private LevelLoader LevelLoader;
    private string[] levels = {"TutorialScene", "CaveGameScene", "ExitRoom"};

    void Start() {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine() {
        yield return new WaitForSeconds(1);
        LevelLoader = Object.FindObjectOfType<LevelLoader>();
    }

     private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.gameObject.CompareTag("Player")) {
            LevelLoader.LoadNextLevel(levels[1]);
            entity.transform.position = new Vector3(0, -6, 0);
        }
    }
}
