using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousSuitcase : Item
{
    [SerializeField] private GameObject effect;
    // Start is called before the first frame update
    public override void OnHit(PlayerInventory playerInventory, GameObject enemy, int stacks){
        int randomChance = Random.Range(1,11);
        if(randomChance <= stacks){
            Instantiate(effect, enemy.transform.position, Quaternion.identity);
            Collider2D[] hitInfo = Physics2D.OverlapCircleAll(enemy.transform.position, 4f, LayerMask.GetMask("Enemy"));
            foreach(Collider2D unit in hitInfo){
                enemy.GetComponent<EnemyDamage>().takeDamage(40);
            } 
        }
    }
}
