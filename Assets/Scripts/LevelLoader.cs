using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    private GameObject exit;

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        switch (SceneManager.GetActiveScene().name) {
            case "TutorialScene":
                SceneManager.LoadScene("CaveGameScene");
                break;
            case "CaveGameScene":
                SceneManager.LoadScene("ExitRoomScene");
                break;
            case "ExitRoomScene":
                SceneManager.LoadScene("CaveGameScene");
                ProcGenDungeon.caveLevel++;
                break;
        }
        Camera.main.GetComponent<CameraMovement>().UpdatePlayerReference();
    }


}
