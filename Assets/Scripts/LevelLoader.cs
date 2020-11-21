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

    public GameObject[] bosses;
    public GameObject[] enemies;
    private Vector3[] enemyLocations = {new Vector3(0, 19, 0), 
                                        new Vector3(4, 16, 9),
                                        new Vector3(-4, 16, 0),
                                        new Vector3(3, 11, 0),
                                        new Vector3(-3, 11, 0)};

    private string[] scenes = {"TutorialScene", "CaveGameScene", "ExitRoomScene"};
    private static int nextScene = 0;
    private static int previousScene = 0;

    private string nextMap;
    private CameraMovement cameraMovement;

    bool loaded = false;
    bool unloaded = false;

    void Start() {
        StartCoroutine(LateStart());
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
    }

    IEnumerator LateStart() {
        yield return new WaitForSeconds(1);

        objectsToMove = GameObject.Find("ObjectsToMove");

        if(SceneManager.GetActiveScene().name.Equals("ExitRoomScene")) {
            Instantiate(bosses[ProcGenDungeon.caveLevel], new Vector3(0, 15, 0), Quaternion.identity, GameObject.Find("Environment").transform);

            for(int i = 0; i < enemyLocations.Length; i++) {
                Instantiate(enemies[ProcGenDungeon.caveLevel], enemyLocations[i], Quaternion.identity, GameObject.Find("Environment").transform);
            }
        }
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
                GameObject.Find("Skip Button").SetActive(false);
                nextMap = "Dynamic Pit";
                break;
            case "CaveGameScene":
                previousScene = 1;
                nextScene = 2;
                nextMap = "Pit";
                break;
            case "ExitRoomScene":
                previousScene = 2;
                nextScene = 1;
                ProcGenDungeon.caveLevel++;
                nextMap = "Dynamic Pit";
                break;
            }
            if(ProcGenDungeon.caveLevel > 2) {
                SceneManager.LoadScene("Outro");
            }
            else {
                SceneManager.LoadSceneAsync(scenes[nextScene], LoadSceneMode.Additive);
                SceneManager.MoveGameObjectToScene(objectsToMove, SceneManager.GetSceneByName(scenes[nextScene]));
            
                if(nextScene == 1) {
                    objectsToMove.transform.GetChild(1).transform.position = new Vector3(0,0,0);
                }
                else if (nextScene == 2) {
                    objectsToMove.transform.GetChild(1).transform.position = new Vector3(0,-6,0);
                }

                if(!unloaded) {
                    unloaded = true;
                    UnloadScene(scenes[previousScene]);
                }
            }
            loaded = true;
            cameraMovement.UpdatePlayerReference(nextMap);
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
