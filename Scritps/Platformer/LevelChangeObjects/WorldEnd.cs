using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnd : MonoBehaviour
{
    public int levelToComplete;
    public int worldImIn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("World" + GameManager.Instance.worldName + "HighestLevel") >= levelToComplete)
            {
                if (PlayerPrefs.GetInt("WorldHubWorldHighestLevel") < worldImIn)
                {
                    PlayerPrefs.SetInt("WorldHubWorldHighestLevel", worldImIn);
                }

                GameManager.Instance.LoadScene("HubWorld");
            }
        }
    }
}
