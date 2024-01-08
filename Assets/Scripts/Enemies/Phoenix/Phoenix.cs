using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Phoenix : EnemyFlyingAI
{
    [SerializeField] private GameObject fireball;

    // Update is called once per frame
    void Update()
    {
        if(canAttack){
            attackingHandler();
        }
    }

    protected override void attackingHandler()
    {
        float distance = Vector2.Distance(attackPoint.transform.position, target.position);
        RaycastHit2D hitInfo = Physics2D.Raycast(attackPoint.transform.position, target.position, distance, layer);
        try{
            if(hitInfo.collider.CompareTag("Player") && canAttack){
                StartCoroutine(startAttack());
            }
        } catch (NullReferenceException e){
            //Isto kao i kod BlueBird
        } 
        
    }

    public IEnumerator startAttack(){
        isAttacking = true;
        canAttack = false;
        animator.SetBool("isAttacking", true);
        animator.SetInteger("State", (int)animationState.attacking);
        yield return new WaitForSeconds(3);
        canAttack = true;
    }

    public override void Attack(){
        GameObject projectile = Instantiate(fireball, attackPoint.transform.position, Quaternion.identity);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        projectile.GetComponent<Fireball>().Enemy = gameObject;
        projectile.GetComponent<Fireball>().Target = target;
    }

}
