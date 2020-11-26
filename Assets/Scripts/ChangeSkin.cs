using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{

    public AnimatorOverrideController maleAnim;
    public AnimatorOverrideController femaleAnim;

    public AnimatorOverrideController male_copperAnim;
    public AnimatorOverrideController male_silverAnim;
    public AnimatorOverrideController male_ironAnim;
    public AnimatorOverrideController male_goldAnim;
    public AnimatorOverrideController male_obsidianAnim;

    public AnimatorOverrideController female_copperAnim;
    public AnimatorOverrideController female_silverAnim;
    public AnimatorOverrideController female_ironAnim;
    public AnimatorOverrideController female_goldAnim;
    public AnimatorOverrideController female_obsidianAnim;

    // Start is called before the first frame update
    void Start()
    {
        switch (MenuFunctions.character) {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = maleAnim as RuntimeAnimatorController;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = femaleAnim as RuntimeAnimatorController;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSkin(){
        switch (MenuFunctions.character) {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = maleAnim as RuntimeAnimatorController;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = femaleAnim as RuntimeAnimatorController;
                break;
        }

    }
    public void CopperSkin()
    {
        switch (MenuFunctions.character)
        {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = male_copperAnim as RuntimeAnimatorController;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = female_copperAnim as RuntimeAnimatorController;
                break;
        }
    }

    public void SilverSkin()
    {
        switch (MenuFunctions.character)
        {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = male_silverAnim as RuntimeAnimatorController;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = female_silverAnim as RuntimeAnimatorController;
                break;
        }
    }

    public void IronSkin()
    {
        switch (MenuFunctions.character)
        {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = male_ironAnim as RuntimeAnimatorController;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = female_ironAnim as RuntimeAnimatorController;
                break;
        }
    }

    public void GoldSkin()
    {
        switch (MenuFunctions.character)
        {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = male_goldAnim as RuntimeAnimatorController;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = female_goldAnim as RuntimeAnimatorController;
                break;
        }
    }

    public void ObsidianSkin()
    {
        switch (MenuFunctions.character)
        {
            case 1:
                GetComponent<Animator>().runtimeAnimatorController = male_obsidianAnim as RuntimeAnimatorController;
                break;
            case 2:
                GetComponent<Animator>().runtimeAnimatorController = female_obsidianAnim as RuntimeAnimatorController;
                break;
        }
    }
}
