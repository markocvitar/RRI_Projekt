using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public GameObject Enemy;
    [SerializeField] private Rigidbody2D rb;
    public float damage;

    private void Start() {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectile"), LayerMask.NameToLayer("Camera"), true);
    }

    void FixedUpdate()
    {
        rb.velocity = (Enemy.transform.position - transform.position) * 20;
        if(!Enemy.GetComponent<BoxCollider2D>().enabled){
            Enemy = Physics2D.OverlapCircle(transform.position, 10f, LayerMask.GetMask("Enemy")).gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<EnemyDamage>().takeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}
