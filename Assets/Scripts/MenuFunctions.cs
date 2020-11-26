using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    // 
    public static int character = 1;
    public GameObject objectsToMove;

    bool loaded = false;
    bool unloaded = false;
   
    void Start(){
        if(SceneManager.GetActiveScene().name == "MainMenu")
        objectsToMove.SetActive(false);
    }
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        
    }

    public void SelectCharacter(int characterSelected) {
        character = characterSelected;
    }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }

    public void LoadGame(){
        if(!loaded) {
            MenuData data = SaveLoad.LoadMenuInfo();
            objectsToMove.SetActive(true);
            character = data.character;
            GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeSkin>().updateSkin();
            ProcGenDungeon.caveLevel = data.caveLevel;
            SceneManager.LoadSceneAsync(data.levelName, LoadSceneMode.Additive);
            if(data.levelName != "TutorialScene"){
                SceneManager.MoveGameObjectToScene(objectsToMove, SceneManager.GetSceneByName(data.levelName));
            }
            if(!unloaded) {
                    unloaded = true;
                    UnloadScene("MainMenu");
                }
            }
        loaded = true;
    }

    public void UnloadScene(string scene) {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(string scene) {
        yield return new WaitForSeconds(1);

        SceneManager.UnloadSceneAsync(scene);
    }

    public void DeathScreenButton() {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }
}
