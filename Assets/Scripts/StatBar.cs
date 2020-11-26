using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatBar : MonoBehaviour
{
    public Slider slider;

    public GameObject segmentPanel;

    public GameObject segmentPrefab;

    private List<GameObject> segments;

    private float segmentSpacing;

    public float statPerSegment;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Check if segments has been initialied
        if(segments == null) { 
            segments = new List<GameObject>();
        }
    }

    // Used at the start of the game to set maximum and current health / maximum and current values
    public void SetMaxStat(int maxStat) {
        // Check if segments has been initialized
        if (segments == null)
        {
            segments = new List<GameObject>();
        }
        else
        {
            foreach (GameObject g in segments)
            {
                Destroy(g);
            }
        }

        segmentSpacing = segmentPanel.GetComponent<GridLayoutGroup>().spacing.x;

        // Set max health to the new max health given and set the slider to show the full health bar(0 = full health)
        slider.maxValue = maxStat;
        slider.value = 0;

        // Determine the number of segments needed as a fraction and a whole number(rounded up) if needed
        float numSegments = maxStat / statPerSegment;
        int wholeSegments = (int)Math.Ceiling(numSegments);

        // Determine the segmentSize
        float containerWidth = segmentPanel.GetComponent<RectTransform>().rect.width;
        float segmentSize = (containerWidth - ((wholeSegments - 1) * segmentSpacing)) / wholeSegments;

        // Set cellSize to segmentSize
        segmentPanel.GetComponent<GridLayoutGroup>().cellSize = new Vector2(segmentSize, 10);
        
        // Instantiate the number of segments needed to represent the players health bar
        for(int i=0; i<(int)numSegments; i++) {
            GameObject newSegment = Instantiate(segmentPrefab);
            newSegment.transform.SetParent(segmentPanel.transform);
            newSegment.transform.localScale = new Vector3(1, 1, 1);
            segments.Add(newSegment);
        }
    }

    public void SetStat(int currentStat, int maxStat) {
        slider.value = maxStat - currentStat;
    }
}
