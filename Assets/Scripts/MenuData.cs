using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MenuData 
{
    public string levelName;
    public int character;


    public MenuData(){
        levelName = SceneManager.GetActiveScene().name;
        character = MenuFunctions.character;
    }
   
}
