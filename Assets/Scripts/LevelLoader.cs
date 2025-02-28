﻿using System.Collections;
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
                                        new Vector3(4, 16, 0),
                                        new Vector3(-4, 16, 0),
                                        new Vector3(3, 11, 0),
                                        new Vector3(-3, 11, 0)};

    private string[] scenes = {"MainMenu", "Intro", "TutorialScene", "CaveGameScene", "ExitRoomScene", "Outro"};
    public int nextScene = 0;
    public  int previousScene = 0;

    public string nextMap;
    private CameraMovement cameraMovement;

    public bool loaded = false;
    public bool unloaded = false;

    public bool enemiesLoaded = false;

    private GameObject tutorialDialogue;
    private GameObject bossRoomDialogue;

    public ChallengeMenu challengeMenu;

    void Start() {
        enemiesLoaded = false;
        StartCoroutine(LateStart());
        cameraMovement = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
        
        challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();

    }

    IEnumerator LateStart() {
        yield return new WaitForSeconds(1.5f);

        if (SceneManager.GetActiveScene().name == "CaveGameScene" && ProcGenDungeon.caveLevel == 0)
        {
            GameObject.Find("Level 1 Alert").GetComponent<DialogueTrigger>().TriggerDialogue();
            Debug.Log("Level 1 Alert");
        }
        else if (SceneManager.GetActiveScene().name == "ExitRoomScene" && ProcGenDungeon.caveLevel == 0)
        {
            GameObject.Find("Boss Room 1 Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
            Debug.Log("Boss Room 1 Dialogue");
        }

        objectsToMove = GameObject.Find("ObjectsToMove");


        // Spawn boss and enemies
        if(SceneManager.GetActiveScene().name.Equals("ExitRoomScene")) {
            
            GameObject newBoss = Instantiate(bosses[ProcGenDungeon.caveLevel], new Vector3(0, 15, 0), Quaternion.identity, GameObject.Find("Environment").transform);
            
            newBoss.name = bosses[ProcGenDungeon.caveLevel].name;
            switch(newBoss.name){
                case "Skeleton":
                    challengeMenu.AddChallenge("Defeat the Skeleton's rat followers", "bossRats", enemyLocations.Length);
                    GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().skelList.Add(newBoss);
                    break;
                case "Vampire":
                    challengeMenu.AddChallenge("Defeat the Vampire's pet bats", "bossBats", enemyLocations.Length);
                    GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().vampList.Add(newBoss);
                    break;
                case "zombie":
                    challengeMenu.AddChallenge("Defeat the Zombie's worm minions", "bossWorms", enemyLocations.Length);
                    GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().zombList.Add(newBoss);
                    break;
                default:
                    break;
            }
            newBoss.layer = 8;
            

            // Spawn enemies and add them to the correct list
            for(int i = 0; i < enemyLocations.Length; i++) {
                GameObject newEnemy = Instantiate(enemies[ProcGenDungeon.caveLevel], enemyLocations[i], Quaternion.identity, GameObject.Find("Environment").transform);

                switch(enemies[ProcGenDungeon.caveLevel].name) {
                    case "Worm":
                        GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().wormList.Add(newEnemy);
                        break;
                    case "Rat":
                        GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().ratList.Add(newEnemy);
                        break;
                    case "Bat":
                        GameObject.FindGameObjectWithTag("Environment").GetComponent<EnemyLists>().batList.Add(newEnemy);
                        break;
                    default:
                        break;
                }
                newEnemy.name = enemies[ProcGenDungeon.caveLevel].name;
                newEnemy.GetComponent<EnemyDrop>().bossRoom = true;
                newEnemy.layer = 8;
            }
            enemiesLoaded = true;
        }

        if(SceneManager.GetActiveScene().name.Equals("CaveGameScene")){
            switch(ProcGenDungeon.caveLevel){
                case 0:
                    challengeMenu.AddChallenge("Defeat atleast 10 worms", "10worm", 10);
                    challengeMenu.AddChallenge("Forge and Equip an Iron Sword after collecting 10 iron", "ironSword");
                    break;
                case 1:
                    challengeMenu.AddChallenge("Squash atleast 10 rats", "10rat", 10);
                    challengeMenu.AddChallenge("Equip a gold sword once you have 10 gold bars", "goldSword");
                    break;
                case 2:
                    challengeMenu.AddChallenge("Vanquish 10  bats!", "10bat", 10);
                    challengeMenu.AddChallenge("Create your final obsidan sword with 10 obsidian", "obsSword");
                    break;
                default:
                    break;
            }
        }
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {

        // close dialogue
        GameObject.Find("UILayer").transform.Find("DialogueBox").gameObject.SetActive(false);


        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        if (!loaded) {
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
                    if (ProcGenDungeon.caveLevel > 2) {
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
                default:
                    break;
            }
            if (previousScene == 1) { // If intro, load tutorial
                SceneManager.LoadScene("TutorialScene");
            }
            else if (nextScene == 5) { // If end of game, load outro
                ProcGenDungeon.caveLevel = 0;
                SceneManager.LoadScene("Outro");
            }
            else if (nextScene == 0) { // If outro, load main menu
                SceneManager.LoadScene("MainMenu");
            }
            else { // Else load next level
                SceneManager.LoadSceneAsync(scenes[nextScene], LoadSceneMode.Additive);
                SceneManager.MoveGameObjectToScene(objectsToMove, SceneManager.GetSceneByName(scenes[nextScene]));

                if (scenes[nextScene].Equals("CaveGameScene")) {
                    objectsToMove.transform.GetChild(1).transform.position = new Vector3(0, 0, 0);
                }
                else if (scenes[nextScene].Equals("ExitRoomScene")) {
                    objectsToMove.transform.GetChild(1).transform.position = new Vector3(0, -6, 0);
                }

                if (!unloaded) {
                    unloaded = true;
                    UnloadScene(scenes[previousScene]);
                }
            }

            //Debug.Log(SceneManager.GetActiveScene().name)


            


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
