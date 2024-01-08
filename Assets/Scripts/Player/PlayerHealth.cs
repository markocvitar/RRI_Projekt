using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [Header("Health")]
    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] public float Health = 100f;
    [SerializeField] private float healCoefficient = 1f;
    [SerializeField] public float damageReduction;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerInventory inventory;

    
    
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("HealthRegen", 1f, 1f);
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate() {
        checkHealth();
    }

    public void TakeDamage(float damage, GameObject source){
        damageSound.Play();
        Health -= damage - damage * damageReduction;
        StartCoroutine("FlashRed");
        foreach(ItemList i in inventory.items){
            i.item.OnTakeDamage(gameObject, source, (int)damage, i.stacks);
        }
    }

    public void Heal(int HealAmount, float healCoefficient){
        if (Health + HealAmount * healCoefficient > MaxHealth){
            Health = MaxHealth;
        } else {
            Health += HealAmount * healCoefficient;
        }
    }

    public void checkHealth(){
        if(Health <= 0){
            foreach(ItemList i in inventory.items){
                i.item.OnDeath(gameObject, i.stacks);
            }
            if(Health <= 0){
                gameObject.SetActive(false);
                Invoke("InvokeGameOver",2f);
            }
            
        }
    }

    void HealthRegen(){
        Heal(1, healCoefficient);
    }

    public IEnumerator FlashRed(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Thorns")){
            TakeDamage(10, null);
        }
    }

    private void InvokeGameOver(){
        GameManager.sharedInstance.LoadGameOver();
    }

}