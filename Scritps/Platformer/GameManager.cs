using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody2D playerRB;

    public Transform cameraTransform;

    public Vector2 playerStartPos;
    public Vector2 playerHubPosition;

    public int gameTime = 0;

    int lives;
    public int coins = 0;
    int time;

    public static bool gameIsOn = false;
    public static bool playerAbleToMove = false;

    public GameObject[] hearts;

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI gameTimeText;

    public List<GameObject> levelObjects;

    public string worldName;

    bool ableToChangeLevels = true;

    public AudioSource musicPlayer, coinAds;
    public AudioClip coinCollectSound;
    
    [HideInInspector]
    public int currentLevel = 0;

    public bool endlessGame;

    private void Start()
    {
        if (!endlessGame)
        {
            GameStart();
        }
        else
        {
            gameIsOn = true;
            playerAbleToMove = true;

            lives = hearts.Length;
            StartCoroutine(Tick(1));
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Invoke("ReloadScene", 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (worldName == "HubWorld" || worldName == "EndlessGame" && currentLevel == 0)
            {
                LoadScene("Menu");
            }
            else if (currentLevel == 0)
            {
                SceneManager.LoadScene("HubWorld");
            }
            else
            {
                ChangeLevels(0);
            }
        }
    }

    public void GameStart()
    {
        LevelManager.Instance.UpdateTexts();

        ChangeLevels(PlayerPrefs.GetInt("CurrentLevel"));
        PlayerPrefs.SetInt("CurrentLevel", 0);

        lives = hearts.Length;
        time = gameTime;

        playerAbleToMove = true;
        gameIsOn = true;

        StartCoroutine(Tick(1));
    }

    IEnumerator Tick(int waitTime)
    {
        while (gameIsOn)
        {
            time++;
            gameTimeText.text = time.ToString();

            yield return new WaitForSeconds(waitTime);
        }
    }

    public void EndGame()
    {
        gameIsOn = false;

        if (endlessGame)
        {      
            if (coins > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", coins);
            }

            LoadScene("Menu");
        }
        else
        {
            LoadScene(worldName);
        }
    }

    public void Pause()
    {
        gameIsOn = false;
    }
    public void UnPause()
    {
        gameIsOn = true;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ReloadScene()
    {
        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetInt("CurrentLevel", currentLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TakeLives(int damageAmount)
    {
        lives -= damageAmount;

        if (lives < 1)
        {
            Destroy(hearts[0].gameObject);
            Invoke("EndGame", 2);

            playerAbleToMove = false;
            gameIsOn = false;
        }
        else if (lives < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (lives < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    public void AddLives(int lifeAddAmount)
    {
        lives += lifeAddAmount;
    }

    public void AddPoints(int addAmount)
    {
        if (addAmount == 0)
        {
            coins = 0;
            pointsText.text = coins.ToString();
        }
        else
        {
            coinAds.Play();

            coins += addAmount;
            pointsText.text = coins.ToString();
        }
    }

    public void ChangeLevels(int level)
    {
        if (ableToChangeLevels)
        {
            AddPoints(0);
            time = 0;
            lives = hearts.Length;

            currentLevel = level;

            playerAbleToMove = false;
            ableToChangeLevels = false;

            playerRB.velocity = new Vector2(0, 0);

            if (level == 0)
            {
                playerTransform.position = new Vector2(PlayerPrefs.GetFloat("World" + worldName + "XPos"), PlayerPrefs.GetFloat("World" + worldName + "YPos") + 2);
            }
            else
            {
                playerTransform.position = playerStartPos;
            }

            cameraTransform.position = new Vector3(playerTransform.position.x, cameraTransform.position.y, cameraTransform.position.z);

            for (int i = 0; i < levelObjects.Count; i++)
            {
                if (i == level)
                {
                    levelObjects[i].SetActive(true);
                }
                else
                {
                    levelObjects[i].SetActive(false);
                }

            }

            Invoke("LevelChangeTimer", 0.5f);
        } 
    }

    void LevelChangeTimer()
    {
        ableToChangeLevels = true;
        playerAbleToMove = true;
    }

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

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
