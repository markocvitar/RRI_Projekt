using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject attackPoint;
    [SerializeField] public LayerMask enemyLayer;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected PlayerInventory playerInventory;
    
    [Header("Damage")]
    [SerializeField] public float meleeDamage;
    [SerializeField] public float rangedDamage;
    [SerializeField] public float damageMeleeModifier;
    [SerializeField] public float damageRangedModifier;
    [SerializeField] public float critChance;
    [SerializeField] public float attackRate; 
  
    //[SerializeField] private GameObject bullet;
    [Header("Animation")]
    [SerializeField] protected Animator animator;

    [Header("Base values")]

    [SerializeField] public float baseAttackRate;
    
    [SerializeField] public bool canUseAbilityOne = true;
    [SerializeField] public bool isUsingAbilityOne = false;
    [SerializeField] public float AbilityOneCooldown;
    [SerializeField] public bool canUseAbilityTwo = true;
    [SerializeField] public bool isUsingAbilityTwo = false;
    [SerializeField] public float AbilityTwoCooldown;


   
    protected enum AnimationState{
        defaultAttack = 4,
        abilityOne = 5,
        abilityTwo = 6
    }

    void Start()
    {
        baseAttackRate = attackRate;
    }

    // Update is called once per frame

    public virtual void attackHandler(){
    }

    public virtual IEnumerator AbilityOne(){
        yield return new();
    }

    public virtual IEnumerator AbilityTwo(){
        yield return new();
    }

}


