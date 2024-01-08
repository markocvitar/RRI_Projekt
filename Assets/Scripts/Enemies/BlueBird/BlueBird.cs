using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BlueBird : EnemyFlyingAI
{

    private GameObject player;
    [SerializeField] private AudioSource chompSound;
    void Update()
    {
        attackingHandler();
    }

    protected override void attackingHandler()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(attackPoint.transform.position, 0.3f, layer);
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
        Collider2D hitInfo = Physics2D.OverlapCircle(attackPoint.transform.position, 0.3f, layer);
        try{
            if(hitInfo.CompareTag("Player")){
                player.GetComponent<PlayerHealth>().TakeDamage(enemyDamage.damage, gameObject);
                StartCoroutine("startAttack");
            }
        } catch (NullReferenceException e){
            //Debug.Log("IDK zasto izbaciva taj exception pa je ovo tu");
        }
        animator.SetBool("isAttacking", false);
        animator.SetInteger("State", (int)animationState.flying);
    }
}
