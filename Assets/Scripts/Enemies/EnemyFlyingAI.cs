using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class EnemyFlyingAI : MonoBehaviour
{
    public Transform target;
    public GameObject attackPoint;
    public bool facingRight = false;
    protected bool canShoot = true;

    public float speed;
    public float nextWaypointDistance = 3f;

    protected Path path;
    [SerializeField] protected int currentWaypoint = 0;
    [SerializeField] protected bool endOfPath = false;
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] protected bool canAttack = true;

    [SerializeField] protected Seeker seeker;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected EnemyDamage enemyDamage;

    protected enum animationState{
        flying = 1,
        attacking = 2
    }

    void Start()
    {
        GetTarget();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    void FixedUpdate(){
        pathHandler();
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    void GetTarget(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void UpdatePath(){
        if(seeker.IsDone()) seeker.StartPath(body.position, target.position, OnPathComplete);
    }


    public void flipGFX(Vector2 force){
        if(force.x < 0 && facingRight){
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        } else if (force.x > 0 && !facingRight) {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }
    }

    void pathHandler(){
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count){
            endOfPath = true;
            return;
        } else {
            endOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
        Vector2 force = direction * speed;

        body.velocity = force;

        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance){
            currentWaypoint ++;
        }

        flipGFX(force);
    }

    protected virtual void attackingHandler(){
    }

    public virtual void Attack(){
    }
}
