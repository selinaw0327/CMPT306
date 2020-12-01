
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ChallengeMenu : MonoBehaviour
{
    public Text challengeText;
    public Text completedText;

    public Text newText;
    private bool GameIsPaused = false;

    public GameObject challengeMenuUI;
    public GameObject challengeCompletedUI;
    public GameObject newChallengeUI;

    private float  timeToCloseCompleted = 4.0f;
    private bool completedOpen = false;
    
    private bool newOpen = false;
    private float  timeToCloseNew = 4.0f;

    public GameObject incomplete;

    private int incompleteChallenges;

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
        
        incomplete = GameObject.FindGameObjectWithTag("Incomplete");
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
        if(newOpen && completedOpen){
            newChallengeUI.SetActive(false);
            if(timeToCloseCompleted > 0){
                timeToCloseCompleted -= Time.unscaledDeltaTime;
            } else {
                closeCompleted();
                newChallengeUI.SetActive(true);
                timeToCloseCompleted = 2.0f;
            }

        } else if(completedOpen){
            if(timeToCloseCompleted > 0){
                timeToCloseCompleted -= Time.unscaledDeltaTime;
            } else {
                closeCompleted();
                timeToCloseCompleted = 2.0f;
            }
        } else if(newOpen){
            if(timeToCloseNew> 0){
                timeToCloseNew -= Time.unscaledDeltaTime;
            } else {
                closeNew();
                timeToCloseNew = 2.0f;
            }
        }

        incompleteChallenges = 0;

        foreach (Challenge c in challengeList)
        {
            if (!c.completed)
            {
                incompleteChallenges++;
            }
        }

        if (incompleteChallenges > 0)
        {
            incomplete.SetActive(true);
            incomplete.transform.Find("Text").GetComponent<Text>().text = incompleteChallenges.ToString();
        }
        else
        {
            incomplete.SetActive(false);
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
        newText.text = description;
        openNew();
        
        
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
            closeNew();
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
        

    }

    public void closeCompleted() {
        completedOpen = false;
        challengeCompletedUI.SetActive(false);
        
    }

    public void openNew() {
        newOpen = true;
        newChallengeUI.SetActive(true);
        

    }

    public void closeNew() {
        newOpen = false;
        newChallengeUI.SetActive(false);
        
    }

    private void OnComplete(string challengeName){
        

        // trigger dialogue
        switch (challengeName)
        {
            case "pickup":
                GameObject.Find("Pickup Dialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
                break;
            case "10cop":
                GameObject.Find("Forge Copper Sword").GetComponent<DialogueTrigger>().TriggerDialogue();
                AddChallenge("Forge a copper Sword with your 10 copper", "copSword");
                
                break;
            case "copSword":
                AddChallenge("Defeat the bat", "batTutorial");
                
                break;
            default:
                break;
        }

    }
}
