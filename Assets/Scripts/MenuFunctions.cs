using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    // 
    public static int character;

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void SelectCharacter(int characterSelected) {
        character = characterSelected;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
