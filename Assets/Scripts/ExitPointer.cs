using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointer : MonoBehaviour
{
    private Vector3 exitPosition;
    private RectTransform pointerRectTransform;

    void Start() {
        StartCoroutine(Coroutine());
    }

    IEnumerator Coroutine() {
        yield return new WaitForSeconds(1);
        exitPosition = GameObject.Find("Exit").transform.position;
        pointerRectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void LateUpdate() {
        Vector3 toPosition = exitPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0.0f;

        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;

        if(pointerRectTransform != null) {
            pointerRectTransform.localEulerAngles = new Vector3(0,0, angle);
        }
    }
}
