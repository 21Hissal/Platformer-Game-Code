using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    public bool constantSpawning;

    public float spawnDelay;
    public float spawnRate;

    public GameObject enemyPrefab;
    GameObject newEnemy;

    public float maxDistanceToPlayer;
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        InvokeRepeating("SpawnEnemy", spawnDelay, spawnRate);
    }

    void SpawnEnemy()
    {
        if (playerTransform.position.x < transform.position.x + maxDistanceToPlayer && playerTransform.position.x > transform.position.x - maxDistanceToPlayer)
        {
            if (!constantSpawning && newEnemy == null)
            {
                newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
            else if (constantSpawning)
            {
                Instantiate(enemyPrefab, transform.position, transform.rotation);
            }
        }
    }
}
