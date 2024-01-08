using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTwoShooting : PlayerShooting
{
    [Header("Class specific")]
    [SerializeField] private ClassTwoMovement movement;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject grenade;
    [SerializeField] private AudioSource blinkAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isUsingAbilityOne || isUsingAbilityTwo) return;
        attackHandler();
        statHandler();
    }

    public override void attackHandler(){
        if(Input.GetKey(KeyCode.E)){
            if(!animator.GetBool("IsAttacking")){
                animator.SetInteger("State",(int)AnimationState.defaultAttack);
            }
        } else {
            animator.SetBool("IsAttacking",false);
        }
        if(Input.GetKeyDown(KeyCode.Q) && canUseAbilityOne){
            StartCoroutine("AbilityOne");
        }
        if(Input.GetKeyDown(KeyCode.R) && canUseAbilityTwo){
            StartCoroutine("AbilityTwo");
        }
    }

    public override IEnumerator AbilityOne()
    {
        canUseAbilityOne = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        isUsingAbilityOne = true;
        animator.SetInteger("State", (int)AnimationState.abilityOne);
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        isUsingAbilityOne = false;
        yield return new WaitForSeconds(AbilityOneCooldown);
        canUseAbilityOne = true;
    }

    public override IEnumerator AbilityTwo()
    {
        canUseAbilityTwo = false;
        isUsingAbilityTwo = true;
        animator.SetInteger("State", 6);
        isUsingAbilityOne = false;
        yield return new WaitForSeconds(AbilityTwoCooldown);
        canUseAbilityTwo = true;
    }
    private void rangedAttack(){
        if(!animator.GetBool("IsAttacking")){
            GameObject bullets = Instantiate(bullet, attackPoint.transform.position, attackPoint.transform.rotation);
            bullets.GetComponent<Bullet>().player = gameObject;
            movement.body.AddForce(transform.right * -2, ForceMode2D.Impulse);
        }
    }

    public void blink(){
        if(movement.facingRight){
            movement.body.MovePosition(new Vector2(transform.position.x + (5 * movement.movementCoefficient), transform.position.y));
        } else {
            movement.body.MovePosition(new Vector2(transform.position.x - (5 * movement.movementCoefficient), transform.position.y));
        }
        blinkAudio.Play();
    }

    public void ThrowGrenade(){
        GameObject grenades = Instantiate(grenade, attackPoint.transform.position, attackPoint.transform.rotation);
        grenades.GetComponent<Grenade>().player = gameObject;
        isUsingAbilityTwo = false;
    }


    public void statHandler(){
        animator.SetFloat("attackRate", attackRate);
    }
}
