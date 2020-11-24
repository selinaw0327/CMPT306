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

    private string[] scenes = {"MainMenu", "Intro", "TutorialScene", "CaveGameScene", "ExitRoomScene", "Outro"};
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
                case "Intro":
                    previousScene = 1;
                    break;
                case "TutorialScene":
                    previousScene = 2;
                    nextScene = 3;
                    GameObject.Find("Skip Button").SetActive(false);
                    nextMap = "Dynamic Pit";
                    break;
                case "CaveGameScene":
                    previousScene = 3;
                    nextScene = 4;
                    nextMap = "Pit";
                    break;
                case "ExitRoomScene":
                    ProcGenDungeon.caveLevel++;
                    if(ProcGenDungeon.caveLevel > 2) {
                        nextScene = 5;
                    }
                    else {
                        previousScene = 4;
                        nextScene = 3;
                    }
                    nextMap = "Dynamic Pit";
                    break;
                case "Outro":
                    previousScene = 5;
                    nextScene = 0;
                    break;
            }
            if(previousScene == 1) { // If intro, load tutorial
                SceneManager.LoadScene("TutorialScene");
            }
            else if(nextScene == 5) { // If end of game, load outro
                SceneManager.LoadScene("Outro");
            }
            else if(nextScene == 0) { // If outro, load main menu
                SceneManager.LoadScene("MainMenu");
            } 
            else { // Else load next level
                SceneManager.LoadSceneAsync(scenes[nextScene], LoadSceneMode.Additive);
                SceneManager.MoveGameObjectToScene(objectsToMove, SceneManager.GetSceneByName(scenes[nextScene]));
            
                if(scenes[nextScene].Equals("CaveGameScene")) {
                    objectsToMove.transform.GetChild(1).transform.position = new Vector3(0,0,0);
                }
                else if (scenes[nextScene].Equals("ExitRoomScene")) {
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
