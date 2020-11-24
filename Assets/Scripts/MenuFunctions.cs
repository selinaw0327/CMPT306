using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    // 
    public static int character = 1;
    public GameObject objectsToMove;
   
    void Start(){
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
        MenuData data = SaveLoad.LoadMenuInfo();
        objectsToMove.SetActive(true);
        character = data.character;
        
        SceneManager.LoadSceneAsync(data.levelName);
        SceneManager.MoveGameObjectToScene(objectsToMove, SceneManager.GetSceneByName(data.levelName));
        
    }
}
