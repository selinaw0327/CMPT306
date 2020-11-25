using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject objectsToMove;

    private bool loaded;
    private bool unloaded;

    void Start(){
        StartCoroutine(SetReferences());
    }

    IEnumerator SetReferences(){
        yield return new WaitForSeconds(0.5f);
        objectsToMove = GameObject.Find("ObjectsToMove");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            ExitButton();
        }
    }

    public void ExitButton()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume () {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause () {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void ExitGame() {
        if(!loaded) {
            objectsToMove.SetActive(false);
            
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
            SceneManager.MoveGameObjectToScene(objectsToMove, SceneManager.GetSceneByName("MainMenu"));
            
            
            if(!unloaded) {
                unloaded = true;
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
                
            }
        }
        loaded = true;
        
    }
}
