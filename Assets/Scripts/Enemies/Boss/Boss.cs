using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject laserPoint;
    [SerializeField] private GameObject fireballs;
    [SerializeField] private int damage;
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        MaxHealth *= GameManager.sharedInstance.stage;
        damage *= GameManager.sharedInstance.stage;
        laserPoint.SetActive(false);
        StartCoroutine("enemyAI");
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        changeText();
    }

    public void takeDamage(int damage){
        Health = Health - damage;
        StartCoroutine("FlashRed");
    }

    private void checkHealth(){
        if(Health <= 0){
            Health = 0;
            Die();
        }
    }

    private void Die(){
        gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
        Invoke("GameWon", 3f);
    }

    public void changeText(){
        healthText.text = Health + "/" + MaxHealth;
    }

    public IEnumerator FlashRed(){
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void GameWon(){
        GameManager.sharedInstance.LoadAboutScene();
    }

    private IEnumerator enemyAI(){
        yield return new WaitForSeconds(3);
        shootFireballs();
        yield return new WaitForSeconds(0.1f);
        shootFireballs();    
        yield return new WaitForSeconds(0.1f);
        shootFireballs(); 
        yield return new WaitForSeconds(0.1f);
        shootFireballs(); 
        yield return new WaitForSeconds(3);
        animator.SetInteger("State", 1);
        yield return new WaitForSeconds(3);
        animator.SetInteger("State", 0);
        laserPoint.SetActive(false);
        StartCoroutine("enemyAI");
    }

    void shootFireballs(){
        GameObject fireball = Instantiate(fireballs, -attackPoint.transform.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().Target = GameManager.sharedInstance.Player.transform;
    }

    void shootLaser(){
        laserPoint.SetActive(true);

    }

}
