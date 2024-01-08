using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Snek : EnemyGroundAI
{
    private GameObject player;
    [SerializeField] private AudioSource chompSound;

    void Update()
    {
        if(canAttack) {
            attackHandler();
        }
    }

    protected override void attackHandler()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(AttackPoint.transform.position, 1, playerLayer);
        try{
            if(hitInfo.CompareTag("Player") && canAttack){
                player = hitInfo.gameObject;
                StartCoroutine("startAttack");
            }
        } catch (NullReferenceException e){
            //Debug.Log("IDK zasto izbaciva taj exception pa je ovo tu");
        }
    }

    public IEnumerator startAttack(){
        canAttack = false;
        animator.SetBool("isAttacking", true);
        animator.SetInteger("State", (int)animationState.attacking);
        yield return new WaitForSeconds(5f);
        canAttack = true;
    }

    public override void Attack(){
        chompSound.Play();
        animator.SetBool("isAttacking", false);
        animator.SetInteger("State", (int)animationState.walking);
        player.GetComponent<PlayerHealth>().TakeDamage(enemyDamage.damage, gameObject);
        Collider2D hitInfo = Physics2D.OverlapCircle(AttackPoint.transform.position, 1, playerLayer);
        try{
            if(hitInfo.CompareTag("Player")){
                player.GetComponent<PlayerHealth>().TakeDamage(enemyDamage.damage, gameObject);
            }
        } catch (NullReferenceException e){
            //Debug.Log("IDK zasto izbaciva taj exception pa je ovo tu");
        }
    }
}
