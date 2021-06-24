using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab = null;
    
    public float enemyMoveSpeed = 10f;
    public float enemySpawnSpeed = 2f;
    
    public Vector3 minSpawnLocation = new Vector3(0, 0, 0);
    public Vector3 maxSpawnLocation = new Vector3(0, 0, 0);

    private float spawnTimer = 0;


    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > enemySpawnSpeed)
        {
            spawnTimer = 0;

            //what sides to spawn the enemy to
            int xPos = Random.Range(0, 2);
            int zPos = Random.Range(0, 2);

            Vector3 spawnLocation = Vector3.zero;
            //negative side of wall
            if (xPos == 0)
            {

            }
            //positive side of wall
            else
            {

            }
            //negative side of wall
            if (zPos == 0)
            {

            }
            //positive side of wall
            else
            {

            }


            //Spawn enemy
            GameObject holder = Instantiate(enemyPrefab, Vector3.zero, 
                Quaternion.identity, transform);

            Enemy tempEnemy = holder.GetComponent<Enemy>();
            tempEnemy.moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            tempEnemy.moveDirection = tempEnemy.moveDirection.normalized;
        }
    }
}
