using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image[] hearts;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public Text gemText;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthDisplay()
    {
        int currentHealth = PlayerHealthController.instance.currentHealth;

        for (int i=0; i < hearts.Length; i++)
        {
            int fullHeartPointsRequired = (i + 1) * 2;
            int diff = currentHealth - fullHeartPointsRequired;

            hearts[i].sprite = diff >= 0 ? heartFull : (diff == -1 ? heartHalf : heartEmpty);
        }
    }

    public void UpdateGemCount()
    {
        gemText.text = LevelManager.instance.gemsCollected.ToString();
    }
}
