using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] public int MaxHealth;
    [SerializeField] private int health;
    [SerializeField] public int damage;
    [SerializeField] public int level;
    [SerializeField] public bool isDead = false;
    [SerializeField] public TextMesh healthText;
    [SerializeField] private AudioSource damageSound;


    [SerializeField] private float damageModifier = 1;
    // Start is called before the first frame update

    void Start(){
        level = GameManager.sharedInstance.stage;
        MaxHealth *= level;
        damage *=  level;
    }
    void Update()
    {
        checkHealth();
        changeText();
    }

    public void takeDamage(int damage){
        health = health - damage;
        StartCoroutine("FlashRed");
    }

    private void checkHealth(){
        if(health <= 0){
            health = 0;
            Die();
        }
    }

    private void Die(){
        GameManager.sharedInstance.playerInventory.Gold += GiveGold();
        gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
    }

    public IEnumerator FlashRed(){
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public int GiveGold(){
        return UnityEngine.Random.Range(10, 31) * level;
    }

    public void changeText(){
        healthText.text = health + "/" + MaxHealth;
    }
}
