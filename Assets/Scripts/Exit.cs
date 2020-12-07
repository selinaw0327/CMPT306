using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private LevelLoader LevelLoader;

    public GameObject promptPrefab;

    public bool prompt;

    private ChallengeMenu challenges;

    private string scene;

    private int level;

    void Start() {
        StartCoroutine(Coroutine());
        challenges = GameObject.Find("TaskMenuCanvas").GetComponent<ChallengeMenu>();

        scene = SceneManager.GetActiveScene().name;
        level = ProcGenDungeon.caveLevel;

        TriggerDialogue(scene, level);
    }

    IEnumerator Coroutine() {
        yield return new WaitForSeconds(1);
        LevelLoader = Object.FindObjectOfType<LevelLoader>();
    }

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.gameObject.CompareTag("Player")) {
            ChallengeMenu challengeMenu = GameObject.FindGameObjectWithTag("Challenges").GetComponent<ChallengeMenu>();
            if(challengeMenu.incompleteChallenges != 0)
            {
                GameObject.Find("Tutorial Exit Checkpoint").GetComponent<DialogueTrigger>().TriggerDialogue();
                return;
            }
            
            if (!prompt)
            {
                GameObject p = Instantiate(promptPrefab, GameObject.Find("UILayer").transform);
                p.name = "Prompt";
                prompt = true;
            }
        }
    }

    public void NextLevel()
    {
        LevelLoader.LoadNextLevel();
    }

    public void TriggerDialogue (string scene, int caveLevel)
    {
        string level = scene + ", " + caveLevel;

        switch (level)
        {
            // first level
            case "CaveGameScene, 0":
                GameObject.Find("Level 1 Alert").GetComponent<DialogueTrigger>().TriggerDialogue();
                break;
            default:
                break;
        }
    }
}
