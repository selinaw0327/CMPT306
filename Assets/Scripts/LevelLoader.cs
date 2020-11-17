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
        // Time.timeScale = 1;
        objectsToMove = GameObject.Find("ObjectsToMove");
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        if(!loaded) {
            switch (SceneManager.GetActiveScene().name) {
            case "TutorialScene":
                previousScene = 0;
                nextScene = 1;
                // Destroy(GameObject.Find("Skip Button"));
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
            
            if(nextScene == 1) {
                objectsToMove.transform.GetChild(2).transform.position = new Vector3(0,0,0);
            }
            else if (nextScene == 2) {
                objectsToMove.transform.GetChild(2).transform.position = new Vector3(0,-6,0);
            }

            if(!unloaded) {
                unloaded = true;
                UnloadScene(scenes[previousScene]);
            }
            loaded = true;
        }
    }

    public void UnloadScene(string scene) {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(string scene) {
        yield return new WaitForSeconds(1);

        SceneManager.UnloadSceneAsync(scene);
    }
}
