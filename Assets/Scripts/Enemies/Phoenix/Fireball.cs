using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject Enemy;
    public Transform Target;
    public bool facingRight = false;
    private int fireballDamage;
    
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyProjectile"), LayerMask.NameToLayer("Enemy"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyProjectile"), LayerMask.NameToLayer("Camera"), true);
        fireballDamage = (int)Enemy.GetComponent<EnemyDamage>().damage;
        body = GetComponent<Rigidbody2D>();
        body.velocity = (Target.transform.position - Enemy.transform.position);
        if(body.velocity.x < 0 && facingRight){
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        } else if (body.velocity.x > 0 && !facingRight) {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }

        Invoke("DestroyProjectile", 3f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(fireballDamage, gameObject);
            Destroy(gameObject);
        } else if (other.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}
