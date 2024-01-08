using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChestSpawnHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> chestSpawnPoints;
    [SerializeField] private List<GameObject> legendaryChestSpawnPoints;
    [SerializeField] private GameObject commonChest;
    [SerializeField] private GameObject rareChest;
    [SerializeField] private GameObject legendaryChest;
    [SerializeField] private int stageCredits;
    [SerializeField] private int commonChestCost;
    [SerializeField] private int rareChestCost;
    [SerializeField] private int legendaryChestCost;


    void Start()
    {
        chestSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("ChestSpawnPoints"));
        legendaryChestSpawnPoints.AddRange(GameObject.FindGameObjectsWithTag("LegendaryChestSpawnPoints"));
        Instantiate(legendaryChest, legendaryChestSpawnPoints[Random.Range(0,legendaryChestSpawnPoints.Count)].transform.position, Quaternion.identity);
        
        while(stageCredits > 0){
            int randomChestSpawnPoint = Random.Range(0, chestSpawnPoints.Count - 1);
            if(chestSpawnPoints[randomChestSpawnPoint].activeSelf){
                int rarity = Random.Range(1,101);
                if(rarity < 70){
                    Instantiate(commonChest, new Vector2(chestSpawnPoints[randomChestSpawnPoint].transform.position.x, chestSpawnPoints[randomChestSpawnPoint].transform.position.y - 0.2f), Quaternion.identity);
                    chestSpawnPoints[randomChestSpawnPoint].SetActive(false);
                    stageCredits -= commonChestCost;
                } else if(rarity < 100){
                    Instantiate(rareChest, new Vector2(chestSpawnPoints[randomChestSpawnPoint].transform.position.x, chestSpawnPoints[randomChestSpawnPoint].transform.position.y - 0.1f), Quaternion.identity);
                    chestSpawnPoints[randomChestSpawnPoint].SetActive(false);
                    stageCredits -= rareChestCost;
                } else if (rarity == 100){
                    Instantiate(legendaryChest, chestSpawnPoints[randomChestSpawnPoint].transform.position, Quaternion.identity);
                    chestSpawnPoints[randomChestSpawnPoint].SetActive(false);
                    stageCredits -= legendaryChestCost;
                }
            }
        }
    }
}
