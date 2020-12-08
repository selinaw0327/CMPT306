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

            if(SceneManager.GetActiveScene().name == "TutorialScene"){
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<Equipped>().equipped != "Swords_Copper")
                {
                    GameObject.Find("Tutorial Exit Checkpoint").GetComponent<DialogueTrigger>().TriggerDialogue();
                    return;
                }
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
        if(SceneManager.GetActiveScene().name != "TutorialScene"){
        EnemyLists enemylists = GameObject.Find("Environment").GetComponent<EnemyLists>();
        enemylists.batList.Clear();
        enemylists.ratList.Clear();
        enemylists.wormList.Clear();
        enemylists.zombList.Clear();
        enemylists.vampList.Clear();
        enemylists.skelList.Clear();
        }
        LevelLoader.LoadNextLevel();
    }

    public void TriggerDialogue (string scene, int caveLevel)
    {
        
    }
}
