using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public string gameSceneName;

    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void StartEndless()
    {
        SceneManager.LoadScene("EndlessGame");
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
