using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Pathfinding;

public class IceColdWater : Item {
    
    public GameObject effect;
    
    public override void OnHit(PlayerInventory playerInventory, GameObject enemy, int stacks)
    {
        StartCoroutine(slowOverTime(stacks, enemy.GetComponent<EnemyDamage>()));
    }
    public IEnumerator slowOverTime(int stacks, EnemyDamage enemy){
        GameObject currentEffect = Instantiate(effect, enemy.transform.position, Quaternion.identity);
        currentEffect.transform.parent = enemy.transform;
        try{
            float originalEnemySpeed = enemy.GetComponent<Snek>().speed;
            enemy.GetComponent<Snek>().speed *= 0.5f;
            new WaitForSeconds(2*stacks);
            enemy.GetComponent<Snek>().speed = originalEnemySpeed;
        } catch (NullReferenceException e){
            Debug.Log("Enemy not flying type");
        }

        try{
            float originalEnemySpeed = enemy.GetComponent<BlueBird>().speed;
            enemy.GetComponent<BlueBird>().speed *= 0.5f;
            new WaitForSeconds(2*stacks);
            enemy.GetComponent<BlueBird>().speed = originalEnemySpeed;
        } catch (NullReferenceException e){
            Debug.Log("Enemy not flying type");
        }

        try{
            float originalEnemySpeed = enemy.GetComponent<Phoenix>().speed;
            enemy.GetComponent<Phoenix>().speed *= 0.5f;
            new WaitForSeconds(2*stacks);
            enemy.GetComponent<Phoenix>().speed = originalEnemySpeed;
        } catch (NullReferenceException e){
            Debug.Log("Enemy not flying type");
        }

        try{
            float originalEnemySpeed = enemy.GetComponent<Shadow>().speed;
            enemy.GetComponent<Shadow>().speed *= 0.5f;
            new WaitForSeconds(2*stacks);
            enemy.GetComponent<Shadow>().speed = originalEnemySpeed;
        } catch (NullReferenceException e){
            Debug.Log("Enemy not flying type");
        }
        Destroy(currentEffect);
        yield return new();
    }
    
}