using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private LevelLoader LevelLoader;

    public GameObject promptPrefab;

    public bool prompt;

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

            if (!prompt)
            {
                GameObject p = Instantiate(promptPrefab, GameObject.Find("UILayer").transform);
                p.name = "Prompt";
                prompt = true;
            }
        }
    }

    public void NextLevel()
    {
        LevelLoader.LoadNextLevel();
    }
}
