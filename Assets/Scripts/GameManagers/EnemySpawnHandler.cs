using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnHandler : MonoBehaviour
{

    [SerializeField] bool SpawningEnemies = false;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] GameObject player;
    [SerializeField] GameManager gameHandler;
    [SerializeField] float timeToSpawn;


    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

        private void Update() {
        if(!SpawningEnemies){
            SpawningEnemies = true;
            timeToSpawn = Random.Range(5f, 21f);
            Invoke("SpawnEnemies", timeToSpawn);
        }
    }

    void SpawnEnemies(){
        SpawningEnemies = false;
        for(int i = 0; i < Random.Range(1,6); i++){
            float randomSpawnX = Random.Range(15, 26);
            float randomSpawnY = Random.Range(15, 26);
            Vector2 spawnPosition = new Vector2(player.transform.position.x + randomSpawnX, player.transform.position.y + randomSpawnY);
            if(!Physics2D.OverlapCircle(spawnPosition, 1f, LayerMask.GetMask("Ground"))){
                GameObject enemy = Instantiate(enemies[Random.Range(0,enemies.Count)], spawnPosition, Quaternion.identity);
                //enemy.GetComponent<EnemyDamage>().level = gameHandler.stage;
            }
        }
        
        
    }
}
