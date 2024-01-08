using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ClassOneShooting : PlayerShooting
{
    [Header("Class specific")]
    [SerializeField] private ClassOneMovement movement;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashDamage;
    [SerializeField] private GameObject Shurikens;
    [SerializeField] private AudioSource class1DashSound;
    [SerializeField] private AudioSource class1AttackSound;
    [SerializeField] private AudioSource class1Shuriken;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if(isUsingAbilityTwo){
            return;
        }
        attackHandler();
        statHandler();
    }

    public override void attackHandler(){
        if(Input.GetKey(KeyCode.E)){
            if(!animator.GetBool("IsAttacking")){
                animator.SetInteger("State", (int) AnimationState.defaultAttack);
            }                    
        } else {
            animator.SetBool("IsAttacking", false);
        }
        if(Input.GetKeyDown(KeyCode.Q) && canUseAbilityOne){
            StartCoroutine(AbilityOne());
        }
        if(Input.GetKeyDown(KeyCode.R) && canUseAbilityTwo){
            StartCoroutine(AbilityTwo());
        }
    }

    public void meleeAttack(){
        class1AttackSound.Play();
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(attackPoint.transform.position, 1, enemyLayer);
        int getCrit = UnityEngine.Random.Range(1,101);
        if(getCrit <= critChance){
            foreach(Collider2D Enemy in hitInfo){
                Enemy.GetComponent<EnemyDamage>().takeDamage((int)((meleeDamage * damageMeleeModifier) * 2));
                foreach(ItemList item in playerInventory.items){
                    item.item.OnHit(playerInventory, Enemy.gameObject, item.stacks);
                }
            }
        } else {
            foreach(Collider2D Enemy in hitInfo){
                Enemy.GetComponent<EnemyDamage>().takeDamage((int)(meleeDamage * damageMeleeModifier));
                foreach(ItemList item in playerInventory.items){
                    item.item.OnHit(playerInventory, Enemy.gameObject, item.stacks);
                }
            }
        }
    }

    public override IEnumerator AbilityOne(){
        class1DashSound.Play();
        canUseAbilityOne = false;
        animator.SetInteger("State", (int)AnimationState.abilityOne);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        float originalGravity = movement.body.gravityScale;
        movement.body.gravityScale = 0;
        isUsingAbilityOne = true;
        Vector2 dashBeginPosition = transform.position;
        Debug.Log(dashBeginPosition);

        if(movement.facingRight){
            movement.body.velocity = new Vector2(transform.localScale.x + (dashForce * movement.movementCoefficient), 0);
        } else {
            movement.body.velocity = new Vector2(transform.localScale.x - (dashForce * movement.movementCoefficient), 0);
        }

        yield return new WaitForSeconds(dashTime);
        isUsingAbilityOne = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Vector2 dashEndPosition = transform.position;
        Debug.Log(dashEndPosition);
        movement.body.gravityScale = originalGravity;
        Collider2D[] hitInfo = Physics2D.OverlapAreaAll(new Vector2(dashEndPosition.x, dashEndPosition.y - 1), new Vector2(dashBeginPosition.x, dashBeginPosition.y + 1), enemyLayer);
        
        int getCrit = UnityEngine.Random.Range(1,101);
        if(getCrit <= critChance){
            foreach(Collider2D Enemy in hitInfo){
                Enemy.GetComponent<EnemyDamage>().takeDamage((int)((meleeDamage * damageMeleeModifier) * 2));
                foreach(ItemList item in playerInventory.items){
                    item.item.OnHit(playerInventory, Enemy.gameObject, item.stacks);
                }
            }
        } else {
            foreach(Collider2D Enemy in hitInfo){
                Enemy.GetComponent<EnemyDamage>().takeDamage((int)(meleeDamage * damageMeleeModifier));
                foreach(ItemList item in playerInventory.items){
                    item.item.OnHit(playerInventory, Enemy.gameObject, item.stacks);
                }
            }
        }

        yield return new WaitForSeconds(AbilityOneCooldown);
        canUseAbilityOne = true;
    }

    public override IEnumerator AbilityTwo(){
        canUseAbilityTwo = false;
        isUsingAbilityTwo = true;
        animator.SetInteger("State", (int)AnimationState.abilityTwo);
        yield return new WaitForSeconds(0.5f);
        isUsingAbilityTwo = false;
        yield return new WaitForSeconds(AbilityTwoCooldown);
        canUseAbilityTwo = true;
    }

    public void ThrowShurikens(){
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);
        class1Shuriken.Play();
        int getCrit = UnityEngine.Random.Range(1,101);
        if(getCrit <= critChance){
            foreach(Collider2D Enemy in hitInfo){
                GameObject shuriken = Instantiate(Shurikens, attackPoint.transform.position, Quaternion.identity);
                shuriken.GetComponent<Shuriken>().Enemy = Enemy.gameObject;
                shuriken.GetComponent<Shuriken>().damage = (rangedDamage * damageRangedModifier) * 2;
                foreach(ItemList item in playerInventory.items){
                    item.item.OnHit(playerInventory, Enemy.gameObject, item.stacks);
                }
            }
        } else {
            foreach(Collider2D Enemy in hitInfo){
                GameObject shuriken = Instantiate(Shurikens, attackPoint.transform.position, Quaternion.identity);
                shuriken.GetComponent<Shuriken>().Enemy = Enemy.gameObject;
                shuriken.GetComponent<Shuriken>().damage = (rangedDamage * damageRangedModifier) * 2;
                foreach(ItemList item in playerInventory.items){
                    item.item.OnHit(playerInventory, Enemy.gameObject, item.stacks);
                }
            }
        }
        hitInfo = null;
    }

    public void statHandler(){
        animator.SetFloat("attackRate", attackRate);
    }
}
