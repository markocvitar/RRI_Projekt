using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suprise : Item
{

    [SerializeField] private List<GameObject> items;
    public override void OnNewStage(GameObject player, int stacks)
    {
        StartCoroutine(giveItem(player));
    }

    private IEnumerator giveItem(GameObject player){
        yield return new WaitForSeconds(2);
        Instantiate(items[Random.Range(0, items.Count)], player.transform.position, Quaternion.identity);
    }
}
