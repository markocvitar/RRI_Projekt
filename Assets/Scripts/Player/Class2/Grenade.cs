using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody2D body;
    public GameObject player;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject effect;
    public int grenadeDamage;
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("Camera"), true);
        grenadeDamage = (int)GameObject.FindGameObjectWithTag("Player").GetComponent<ClassTwoShooting>().rangedDamage * 3;
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * 10 + new Vector3(0,1.5f) ,ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        
    }   

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Enemy")){
            Explode();
        } else Invoke("Explode", 2f);
    }

    private void Explode(){
        Instantiate(effect, transform.position, Quaternion.identity);
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, 4, enemyLayer);
        foreach(Collider2D enemy in hitInfo){
            enemy.gameObject.GetComponent<EnemyDamage>().takeDamage(grenadeDamage);
            foreach(ItemList item in player.GetComponent<PlayerInventory>().items){
                item.item.OnHit(player.GetComponent<PlayerInventory>(), enemy.gameObject, item.stacks);
            }
        }
        Destroy(gameObject);
    }
}
