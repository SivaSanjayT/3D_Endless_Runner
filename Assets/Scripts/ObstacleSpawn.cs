using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    [Header("GameObjectArray")]
    public GameObject[] ObstaclePrefabs;

    [Header("Random values")]
    public float MinValX = -5f;
    public float MaxValX = 5f;
    public float spawnDistance = 30f;

    public float spawnInterval = 2f;

    [Header("playerTransform")]
    public Transform playerTransform;

    private float timer;
    private void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timer = spawnInterval;
    }

    void Update()
    {

        if (Player.isplayerDied)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f && !Player.isplayerDied)
        {
            ObstcaleSpawning();
            timer = spawnInterval;
        }

        DestroyObstaclesBehindPlayer();
    }
    public void ObstcaleSpawning()
    {
        int randomIndex = Random.Range(0, ObstaclePrefabs.Length);

        Vector3 SpawnPos = new Vector3(Random.Range(MinValX, MaxValX), 0, playerTransform.position.z + spawnDistance);
        Instantiate(ObstaclePrefabs[randomIndex], SpawnPos, Quaternion.identity);
    }

    void DestroyObstaclesBehindPlayer()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacles");

        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle.transform.position.z < playerTransform.position.z - 10f)
            {
                print("obstacle dead");
                Destroy(obstacle);
            }
        }
    }


}
