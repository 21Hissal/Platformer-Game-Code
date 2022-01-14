using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public int level;
    [HideInInspector]
    public int coins;

    public List<GameObject> levelChangers;

    [HideInInspector]
    public List<TextMeshPro> levelTexts;
    [HideInInspector]
    public List<LevelChangeObject> levelObjectScripts;

    private void Start()
    {
        for (int i = 0; i < levelChangers.Count; i++)
        {
            levelObjectScripts.Add(levelChangers[i].GetComponent<LevelChangeObject>());
            levelTexts.Add(levelChangers[i].GetComponentInChildren<TextMeshPro>());
        }

        UpdateTexts();
        UpdateLevelObjects();
    }

    public void UpdateTexts()
    {
        for (int i = 0; i < levelTexts.Count; i++)
        {
            int levelNumber = i + 1;
            levelTexts[i].text = PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "level" + levelNumber.ToString() + "Coins").ToString() + "/3";
        }

        level = 0;
        coins = 0;
    }

    public void UpdateLevelObjects()
    {
        for (int i = 0; i < levelObjectScripts.Count; i++)
        {
            int levelNumber = i + 1;

            if (levelObjectScripts[i].levelToGoTo <= PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "HighestLevel") + 1)
            {
                levelChangers[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
            else
            {
                levelChangers[i].GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }
    }

    private static LevelManager instance;

    public static LevelManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
