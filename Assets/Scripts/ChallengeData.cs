using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ChallengeData 
{

    
    public List<ChallengeMenu.Challenge> challengeList;


    public ChallengeData(ChallengeMenu challenges){
        challengeList = challenges.challengeList;
    }
}
