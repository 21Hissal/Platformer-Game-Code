using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public int levelImIn;
    
    private int levelToGoTo = 0;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.coins > PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "level" + levelImIn.ToString() + "Coins"))
            {
                PlayerPrefs.SetInt("World" + GameManager.Instance.worldName + "level" + levelImIn.ToString() + "Coins", GameManager.Instance.coins);
            }
            
            LevelManager.Instance.level = levelImIn;
            LevelManager.Instance.coins = GameManager.Instance.coins;
            LevelManager.Instance.UpdateTexts();
            

            if (PlayerPrefs.GetInt("World"+ GameManager.Instance.worldName + "HighestLevel") < levelImIn)
            {
                PlayerPrefs.SetInt("World" + GameManager.Instance.worldName + "HighestLevel", levelImIn);
            }

            GameManager.Instance.ChangeLevels(levelToGoTo);

            LevelManager.Instance.UpdateLevelObjects();


        }
    }
}

