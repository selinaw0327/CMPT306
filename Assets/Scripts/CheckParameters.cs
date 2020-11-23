using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckParameters : MonoBehaviour
{
    public bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
        if (param.name == paramName)
            return true;
        }
        return false;
    }
}
