using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    private GameObject exit;

    public void LoadNextLevel(string nextLevel) {
        StartCoroutine(LoadLevel(nextLevel));
    }

    IEnumerator LoadLevel(string nextLevel) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
