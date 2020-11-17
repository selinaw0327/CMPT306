using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader levelLoader;
    public Animator transition;

    private GameObject exit;

    public GameObject objectsToMove;

    private string[] scenes = {"TutorialScene", "CaveGameScene", "ExitRoomScene"};
    private static int nextScene = 0;
    private static int previousScene = 0;

    bool loaded = false;
    bool unloaded = false;

    void Start() {
        objectsToMove = GameObject.Find("ObjectsToMove");

        if(scenes[nextScene].Equals("CaveGameScene")) {
            Debug.Log("Hello");
            objectsToMove.GetComponent<SaveLoadRunner>().map = GameObject.Find("Grid").GetComponent<ProcGenDungeon>();            
            objectsToMove.GetComponent<SaveLoadRunner>().rockList = GameObject.Find("Environment").GetComponent<RockList>();
            objectsToMove.GetComponent<SaveLoadRunner>().enemyLists = GameObject.Find("Environment").GetComponent<EnemyLists>();
        }
    }

    public void LoadNextLevel() {
        transition.SetTrigger("Start");

        if(!loaded) {
            switch (SceneManager.GetActiveScene().name) {
            case "TutorialScene":
                previousScene = 0;
                nextScene = 1;
                break;
            case "CaveGameScene":
                previousScene = 1;
                nextScene = 2;
                break;
            case "ExitRoomScene":
                previousScene = 2;
                nextScene = 1;
                ProcGenDungeon.caveLevel++;
                break;
            }

            SceneManager.LoadSceneAsync(scenes[nextScene], LoadSceneMode.Additive);

            SceneManager.MoveGameObjectToScene(objectsToMove, SceneManager.GetSceneByName(scenes[nextScene]));
            
            objectsToMove.transform.GetChild(2).transform.position = new Vector3(0,0,0);
            if(!unloaded) {
                unloaded = true;
                UnloadScene(scenes[previousScene]);
            }
            loaded = true;
        }
    }

    // IEnumerator LoadLevel() {
    //     transition.SetTrigger("Start");

    //     yield return new WaitForSeconds(1);

    //     if(!loaded) {
    //         switch (SceneManager.GetActiveScene().name) {
    //         case "TutorialScene":
    //             previousScene = 0;
    //             nextScene = 1;
    //             // SceneManager.LoadScene(scenes[nextScene]);
    //             break;
    //         case "CaveGameScene":
    //             previousScene = 1;
    //             nextScene = 2;
    //             // SceneManager.LoadSceneAsync(scenes[nextScene], LoadSceneMode.Additive);
    //             break;
    //         case "ExitRoomScene":
    //             previousScene = 2;
    //             nextScene = 1;
    //             // SceneManager.LoadScene(scenes[nextScene]);
    //             ProcGenDungeon.caveLevel++;
    //             break;
    //         }
    //         SceneManager.LoadSceneAsync(scenes[nextScene], LoadSceneMode.Additive);


    //         SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(scenes[nextScene]));
    //         SceneManager.MoveGameObjectToScene(mainCamera, SceneManager.GetSceneByName(scenes[nextScene]));
    //         SceneManager.MoveGameObjectToScene(menus, SceneManager.GetSceneByName(scenes[nextScene]));
    //         SceneManager.MoveGameObjectToScene(mapCamera, SceneManager.GetSceneByName(scenes[nextScene]));
    //         player.transform.position = new Vector3(0,0,0);
    //         if(!unloaded) {
    //             unloaded = true;
    //             UnloadScene(scenes[previousScene]);
    //         }
    //         loaded = true;
    //     }
    // }

    public void UnloadScene(string scene) {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(string scene) {
        yield return new WaitForSeconds(1);

        SceneManager.UnloadSceneAsync(scene);
    }
}
