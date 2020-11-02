using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMenu : MonoBehaviour
{
    public Text challengeText;
    
    private bool GameIsPaused = false;

    public GameObject challengeMenuUI;
    
    [System.Serializable]
    public struct Challenge
    {
        public string description;
        public bool completed;
        public string name;

    }

    public List<Challenge> challengeList = new List<Challenge>();


    void Start()
    {

        AddChallenge("Hit I to open and close your Inventory","inv");
        AddChallenge("Pick up an Item", "pickup");
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            if(!GameIsPaused){
                challengeText.text = "";
                foreach(Challenge c in challengeList ){
                    if(!c.completed){
                        challengeText.text += ("\t Challenge: "+c.description+"\n");
                    }
                }
                Pause();
            } else {
                Resume();
            }      
        }
    }
    public void AddChallenge(string description, string name){
        Challenge newChallenge = new Challenge();
        newChallenge.description = description;
        newChallenge.completed = false;
        newChallenge.name = name;
        challengeList.Add(newChallenge);
    }   

    public  void updateChallenge(string name){
        int  challengeIndex = challengeList.FindIndex(challenge => challenge.name == name);
        if(challengeIndex!=-1){
            Challenge challenge = challengeList[challengeIndex];
            if(!challenge.completed){
                challenge.completed = true;
                challengeList[challengeIndex] = challenge;
                if(!challengeList[challengeIndex].completed){
                    Debug.Log("Error completed challenge still in the list uncompleted in the list");
                }
            }
        } else {
            Debug.Log("Error tried to update challenge that does not exist");
        }
        
    }

    public void Resume () {
        challengeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause () {
        challengeMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

}
