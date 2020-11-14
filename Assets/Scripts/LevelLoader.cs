using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader levelLoader;
    public Animator transition;

    private GameObject exit;

    public GameObject player, mainCamera, menus, mapCamera;

    bool loaded;
    bool unloaded;

    public void LoadNextLevel() {
        transition.SetTrigger("Start");

        if(!loaded) {
            switch (SceneManager.GetActiveScene().name) {
            case "TutorialScene":
                SceneManager.LoadScene("CaveGameScene");
                break;
            case "CaveGameScene":
                SceneManager.LoadSceneAsync("ExitRoomScene", LoadSceneMode.Additive);
                break;
            case "ExitRoomScene":
                SceneManager.LoadScene("CaveGameScene");
                ProcGenDungeon.caveLevel++;
                break;
            }
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("ExitRoomScene"));
            SceneManager.MoveGameObjectToScene(mainCamera, SceneManager.GetSceneByName("ExitRoomScene"));
            SceneManager.MoveGameObjectToScene(menus, SceneManager.GetSceneByName("ExitRoomScene"));
            SceneManager.MoveGameObjectToScene(mapCamera, SceneManager.GetSceneByName("ExitRoomScene"));
            player.transform.position = new Vector3(0,-6,0);
            if(!unloaded) {
                unloaded = true;
                UnloadScene("CaveGameScene");
            }
            loaded = true;
        }

        // StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        if(!loaded) {
            switch (SceneManager.GetActiveScene().name) {
            case "TutorialScene":
                SceneManager.LoadScene("CaveGameScene");
                break;
            case "CaveGameScene":
                SceneManager.LoadSceneAsync("ExitRoomScene", LoadSceneMode.Additive);
                break;
            case "ExitRoomScene":
                SceneManager.LoadScene("CaveGameScene");
                ProcGenDungeon.caveLevel++;
                break;
            }
            SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("ExitRoomScene"));
            SceneManager.MoveGameObjectToScene(mainCamera, SceneManager.GetSceneByName("ExitRoomScene"));
            SceneManager.MoveGameObjectToScene(menus, SceneManager.GetSceneByName("ExitRoomScene"));
            player.transform.position = new Vector3(0,-6,0);
            if(!unloaded) {
                unloaded = true;
                UnloadScene("CaveGameScene");
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
