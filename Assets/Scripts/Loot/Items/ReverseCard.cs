using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ReverseCard : Item
{
    public override void OnTakeDamage(GameObject player, GameObject source, int damage, int stacks)
    {
        int randomInt = UnityEngine.Random.Range(1,21);
        if(randomInt <= stacks){
            player.GetComponent<PlayerHealth>().Heal(damage, 1);
            try{
                GameObject enemy = source.GetComponent<Fireball>().Enemy;
                enemy.GetComponent<EnemyDamage>().takeDamage(damage);
            } catch (NullReferenceException e){
                
            }

            try{
                GameObject enemy = source.GetComponent<ShadowFlame>().Enemy;
                enemy.GetComponent<EnemyDamage>().takeDamage(damage);
            } catch (NullReferenceException e){
                
            }

            try{
                source.GetComponent<EnemyDamage>().takeDamage(damage);
            } catch (NullReferenceException e){
                
            }
        }
    }
}
