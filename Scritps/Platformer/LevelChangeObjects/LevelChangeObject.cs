using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeObject : MonoBehaviour
{
    public int levelToGoTo;

    Animator anim;

    bool playerOnTrigger;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerOnTrigger)
        {
            PlayerPrefs.SetFloat("World" + GameManager.Instance.worldName + "XPos", transform.position.x);
            PlayerPrefs.SetFloat("World" + GameManager.Instance.worldName + "YPos", transform.position.y);

            GameManager.Instance.ChangeLevels(levelToGoTo);

            playerOnTrigger = false;

            anim.SetBool("Open", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "HighestLevel") >= levelToGoTo - 1)
            {
                playerOnTrigger = true;

                anim.SetBool("Open", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "HighestLevel") >= levelToGoTo - 1)
            {
                playerOnTrigger = false;

                anim.SetBool("Open", false);
            }
        }
    }
}
