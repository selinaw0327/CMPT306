using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{

    public AnimatorOverrideController copperAnim;
    public AnimatorOverrideController silverAnim;
    public AnimatorOverrideController ironAnim;
    public AnimatorOverrideController goldAnim;
    public AnimatorOverrideController obsidianAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CopperSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = copperAnim as RuntimeAnimatorController;
    }

    public void SilverSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = silverAnim as RuntimeAnimatorController;
    }

    public void IronSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = ironAnim as RuntimeAnimatorController;
    }

    public void goldSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = goldAnim as RuntimeAnimatorController;
    }

    public void ObsidianSkin()
    {
        GetComponent<Animator>().runtimeAnimatorController = obsidianAnim as RuntimeAnimatorController;
    }


}
