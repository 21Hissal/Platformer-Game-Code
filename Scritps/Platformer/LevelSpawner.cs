using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public List<GameObject> firstLevels, secondLevels, thirdLevels;
    float spawnPosX;
    int metaLevel;
    int currentLevel;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosX = 18;
        currentLevel = 0;
        metaLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x > spawnPosX - 18)
        {
            SpawnLevel();
        }
        
    }

    void SpawnLevel()
    {
        List<GameObject> spawnables;

        switch (metaLevel)
        {
            case 1:
                spawnables = firstLevels;
                break;
            case 2:
                spawnables = secondLevels;
                break;
            case 3:
                spawnables = thirdLevels;
                break;

            default:
                int randomNumber = Random.Range(0, 3);

                print(randomNumber);

                switch (randomNumber)
                {
                    case 0:
                        spawnables = firstLevels;
                        break;
                    case 1:
                        spawnables = secondLevels;
                        break;
                    case 2:
                        spawnables = thirdLevels;
                        break;

                    default:
                        spawnables = thirdLevels;
                        break;
                }
                break;
        }

        Instantiate(spawnables[Random.Range(0, spawnables.Count)], Vector3.right * spawnPosX, transform.rotation);
        currentLevel++;
        spawnPosX += 18;

        if (currentLevel % 5 == 0)
        {
            metaLevel++;
        }
        //else if (metaLevel == 2 && currentLevel % 10 == 0)
        //{
        //    metaLevel++;
        //}
        //else if (metaLevel == 3 && currentLevel % 10 == 0)
        //{
        //    metaLevel++;
        //}
    }
}
