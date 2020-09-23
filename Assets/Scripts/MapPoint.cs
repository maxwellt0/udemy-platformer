using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, right, down, left;
    public bool isLevel, isLocked;
    public string levelToLoad, levelToCheck, levelName;

    public int gemsCollected, gemsTotal;
    public float timeBest, timeTarget;
    public GameObject gemBadge, timeBadge;

    // Start is called before the first frame update
    private void Start()
    {
        if (isLevel && levelToLoad != null)
        {
            if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }
            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                timeBest = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }
            if (gemsCollected >= gemsTotal)
            {
                gemBadge.SetActive(true);
            }
            if (timeBest <= timeTarget && timeBest != 0)
            {
                timeBadge.SetActive(true);
            }

            isLocked = levelToCheck == null ||
                !PlayerPrefs.HasKey(levelToCheck + "_unlocked")
                || PlayerPrefs.GetInt(levelToCheck + "_unlocked") != 1;

            if (levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
