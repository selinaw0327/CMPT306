using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeMenu : MonoBehaviour
{
    public Text challengeText;
    private bool show = false;

    public GameObject challengeMenuUI;
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
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)){
            Debug.Log("LeftControl hit");
            challengeText.text = "\n";
            foreach(Challenge c in challengeList ){
                if(!c.completed){
                    challengeText.text += ("\t"+c.description+"\n");
                    
                }
            }
            challengeMenuUI.SetActive(!show);
            show = !show;
            
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
            }
        }
        
    }


}
