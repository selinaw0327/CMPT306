using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMenu : MonoBehaviour
{
    public Text challengeText;
    public Text completedText;
    
    private bool GameIsPaused = false;

    public GameObject challengeMenuUI;
    public GameObject challengeCompletedUI;
    public delegate void OnComplete(); 
    //type for challenges to calll void functions when they are completed
    //so specific events can happen when ceratain challenges are completed

    
    [System.Serializable]
    public struct Challenge
    {
        public string description;
        public bool completed;
        public string name;

        public int count;
        public bool countable;

        public OnComplete oncompleteMethod;
        
    }

    public List<Challenge> challengeList = new List<Challenge>();


    void Start()
    {

        AddChallenge("Hit I to open and close your Inventory","inv");
        AddChallenge("Pick up an Item", "pickup");
        AddChallenge("Collect 10 copper bars", "10cop", 10, cop10OnComplete);
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            ShowHide();
        }
        challengeText.text = "";
        foreach (Challenge c in challengeList)
        {
            if (!c.completed)
            {
                challengeText.text += ("\t Challenge: " + c.description );
                if(c.countable){
                    challengeText.text += " ( " + c.count + " left ) ";
                }
                challengeText.text += "\n\n";
            }
        }
    }
    public void AddChallenge(string description, string name, int count = 1, OnComplete onComplete = null){
        Challenge newChallenge = new Challenge();
        newChallenge.description = description;
        newChallenge.completed = false;
        newChallenge.name = name;
        newChallenge.count = count;
        newChallenge.countable = count!=1; 
        newChallenge.oncompleteMethod = onComplete;
        
        challengeList.Add(newChallenge);
    }   

    public  void updateChallenge(string name){
        int  challengeIndex = challengeList.FindIndex(challenge => challenge.name == name);
        if(challengeIndex!=-1){
            Challenge challenge = challengeList[challengeIndex];
            bool completedChallenge = false;
            if(!challenge.completed){ 
                if(challenge.countable){
                    int origCount = challenge.count;
                    challenge.count -= 1;
                    if(challenge.count == 0){
                        completedChallenge = true;
                    }
                } else {
                    completedChallenge = true;
                }
            }
            
            if(completedChallenge){
                challenge.completed = true;
                completedText.text = challenge.description;
                openCompleted();
                if(challenge.oncompleteMethod!= null){
                    challenge.oncompleteMethod();
                }
            }
            challengeList[challengeIndex] = challenge;
            
        } else {
            Debug.Log("Error tried to update challenge that does not exist");
        }
        
    }

    public void ShowHide()
    {
        if (!GameIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Resume () {
        challengeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause () {
        challengeMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void openCompleted() {
        challengeCompletedUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeCompleted() {
        challengeCompletedUI.SetActive(false);
        Time.timeScale = 1f;
    }


    private void  cop10OnComplete(){
        AddChallenge("Kill 5 bats", "5bat", 5);
    }
}
