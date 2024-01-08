using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D body;
    public GameObject player;
    private int bulletDamage;
    
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("Camera"), true);
        bulletDamage = (int)player.GetComponent<ClassTwoShooting>().rangedDamage;
        body = GetComponent<Rigidbody2D>();
        body.AddForce(transform.right * 50f ,ForceMode2D.Impulse);    
    }

    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Enemy")){
            other.gameObject.GetComponent<EnemyDamage>().takeDamage(bulletDamage);
            foreach(ItemList item in player.GetComponent<PlayerInventory>().items){
                item.item.OnHit(player.GetComponent<PlayerInventory>(), other.gameObject, item.stacks);
            }
        }
        Destroy(gameObject);
    }
}
