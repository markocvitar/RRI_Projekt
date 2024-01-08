using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Shadow : EnemyGroundAI
{
    [SerializeField] private GameObject shadowFlame;
    [SerializeField] private Transform target;

    // Update is called once per frame
    
    void Start(){
        GetTarget();
    }

    void Update()
    {
        if(canAttack){
            attackHandler();
        }
    }

    protected override void attackHandler()
    {
        float distance = Vector2.Distance(AttackPoint.transform.position, target.position);
        RaycastHit2D hitInfo = Physics2D.Raycast(AttackPoint.transform.position, target.position, distance, playerLayer);
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
        GameObject projectile = Instantiate(shadowFlame, AttackPoint.transform.position, Quaternion.identity);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetInteger("State", (int)animationState.walking);
        projectile.GetComponent<ShadowFlame>().Enemy = gameObject;
        projectile.GetComponent<ShadowFlame>().Target = target;
    }

    void GetTarget(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
