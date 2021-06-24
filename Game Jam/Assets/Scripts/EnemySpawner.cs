using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab = null;
    
    public float enemyMoveSpeed = 10f;
    public float enemySpawnSpeed = 2f;
    public float ySpawnHeight = 0.15f;

    //small box of directions to give the enemies better movement
    public Vector3 minMoveLocation = new Vector3(0, 0, 0);
    public Vector3 maxMoveLocation = new Vector3(0, 0, 0);

    public float xSpawnPosition = 0;
    public float zSpawnPosition = 0;

    //spawn ranges of the walls
    public float minXSpawn = 0;
    public float maxXSpawn = 0;
    public float minZSpawn = 0;
    public float maxZSpawn = 0;

    private float spawnTimer = 0;


    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > enemySpawnSpeed)
        {
            spawnTimer = 0;

            //what sides to spawn the enemy to
            int wallChoice = Random.Range(0, 4);
            
            Vector3 spawnLocation = Vector3.zero;
            //Left wall
            if (wallChoice == 0)
            {
                spawnLocation = new Vector3(xSpawnPosition, ySpawnHeight, Random.Range(minZSpawn, maxZSpawn));
            }
            //Right wall
            else if (wallChoice == 1)
            {
                spawnLocation = new Vector3(-xSpawnPosition, ySpawnHeight, Random.Range(minZSpawn, maxZSpawn));
            }
            //Up wall
            else if (wallChoice == 2)
            {
                spawnLocation = new Vector3(Random.Range(minXSpawn, maxXSpawn), ySpawnHeight, zSpawnPosition);
            }
            //Down wall
            else if (wallChoice == 3)
            {
                spawnLocation = new Vector3(Random.Range(minXSpawn, maxXSpawn), ySpawnHeight, -zSpawnPosition);
            }


            //Spawn enemy
            GameObject holder = Instantiate(enemyPrefab, spawnLocation, 
                Quaternion.identity, transform);

            Enemy tempEnemy = holder.GetComponent<Enemy>();
            
            //have a area in the middle to make all enemys move towards that (so they dont spawn and go straight for a wall behind them)
            Vector3 direction = new Vector3(
                Random.Range(minMoveLocation.x, maxMoveLocation.x), 
                0, 
                Random.Range(minMoveLocation.z, maxMoveLocation.x));
            Vector3 enemyPosition = tempEnemy.transform.position;
            
            //set to 0 so its only moving along the x and z axis as it is top down
            enemyPosition.y = 0;

            tempEnemy.moveDirection = (direction - enemyPosition).normalized;
            
        }
    }
}
