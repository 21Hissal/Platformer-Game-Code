using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelect : MonoBehaviour
{
    public string worldToGoTo;
    public int WorldNumber;

    bool playerOnTrigger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerOnTrigger)
        {
            PlayerPrefs.SetFloat("World" + GameManager.Instance.worldName + "XPos", transform.position.x);
            PlayerPrefs.SetFloat("World" + GameManager.Instance.worldName + "YPos", transform.position.y);

            playerOnTrigger = false;

            GameManager.Instance.LoadScene(worldToGoTo);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "HighestLevel") >= WorldNumber - 1)
            {
                playerOnTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "HighestLevel") >= WorldNumber - 1)
            {
                playerOnTrigger = false;
            }
        }
    }
}
