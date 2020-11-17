
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

    private float  timeToCloseCompleted = 4.0f;
    private bool completedOpen = false;

    [System.Serializable]
    public struct Challenge
    {
        public string description;
        public bool completed;
        public string name;

        public int count;
        public bool countable;
    }


    public List<Challenge> challengeList = new List<Challenge>();


    void Start()
    {
        AddChallenge("Hit I to open and close your Inventory","inv");
        AddChallenge("Pick up an Item", "pickup");
        AddChallenge("Collect 10 copper bars", "10cop", 10);
        
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
        if(completedOpen){
            if(timeToCloseCompleted > 0){
                timeToCloseCompleted -= Time.unscaledDeltaTime;
            } else {
                closeCompleted();
                timeToCloseCompleted = 2.0f;
            }
        }
    }
    
    public void AddChallenge(string description, string name, int count = 1){
        Challenge newChallenge = new Challenge();
        newChallenge.description = description;
        newChallenge.completed = false;
        newChallenge.name = name;
        newChallenge.count = count;
        newChallenge.countable = count!=1; 
        
        
        challengeList.Add(newChallenge);
    }   

    public void incrementChallenge(string name ){
        int  challengeIndex = challengeList.FindIndex(challenge => challenge.name == name);
        if(challengeIndex!=-1){
            Challenge challenge = challengeList[challengeIndex];
            if(challenge.countable && !challenge.completed){ 
                challenge.count += 1;       
            }
            challengeList[challengeIndex] = challenge;
            
        } else {
            Debug.Log("Error tried to update challenge that does not exist");
        }
    
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
                OnComplete(challenge.name);
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
            closeCompleted();
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
        completedOpen = true;
        challengeCompletedUI.SetActive(true);
        GameIsPaused = true;

    }

    public void closeCompleted() {
        completedOpen = false;
        challengeCompletedUI.SetActive(false);
        GameIsPaused = false;
    }

    private void OnComplete(string challengeName){
        if(challengeName == "10cop"){
            AddChallenge("Kill 5 bats", "5bat", 5);
        }

    }
}
