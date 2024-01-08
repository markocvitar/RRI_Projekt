using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouldyBread : Item
{
    [SerializeField] private GameObject effect;

    public override void OnHit(PlayerInventory playerInventory, GameObject enemy, int stacks)
    {
        StartCoroutine(damageOverTime(stacks, enemy.GetComponent<EnemyDamage>()));
    }

    public IEnumerator damageOverTime(int stacks, EnemyDamage enemy){
        GameObject currentEffect = Instantiate(effect, enemy.transform.position, Quaternion.identity);
        currentEffect.transform.parent = enemy.transform;
        yield return new WaitForSeconds(1);
        for(int i = 0; i < stacks * 3; i++){
            if (enemy.isActiveAndEnabled){
                enemy.takeDamage((int)(enemy.MaxHealth * (0.05f * stacks)));
                yield return new WaitForSeconds(1);
            }
        }
        Destroy(currentEffect);
    }
}
