using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGroundAI : MonoBehaviour
{
    [SerializeField] protected bool mustPatrol;
    [SerializeField] protected bool mustJump;
    [SerializeField] protected bool mustTurn;
    [SerializeField] protected bool noGround;
    [SerializeField] protected bool hitWallBottom;
    [SerializeField] protected bool hitWallTop;

    [SerializeField] public float speed;
    
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform WallCheckBottom;
    [SerializeField] protected Transform WallCheckTop;

    [SerializeField] protected Transform AttackPoint;


    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask playerLayer;

    [SerializeField] protected Rigidbody2D body;

    [SerializeField] protected bool canAttack = true;
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] protected bool facingRight = false;

    [SerializeField] protected Animator animator;
    [SerializeField] protected EnemyDamage enemyDamage;
    protected enum animationState{
        walking = 1,
        attacking = 2
    }


    void Start()
    {
        mustPatrol = true;
    }

    void FixedUpdate()
    {
        Patrol();
        attackHandler();
        mustTurn = !CheckForGround() || (CheckForGround() && CheckForBottomWall() && CheckForTopWall()) || CheckForTopWall();
        mustJump = CheckForGround() && CheckForBottomWall() && !CheckForTopWall();
    }

    protected void Patrol(){
        if(mustTurn) Flip();
        if(mustJump) Jump();
        if(facingRight) body.velocity = new Vector2(speed, body.velocity.y);
        else body.velocity = new Vector2(-speed, body.velocity.y);
    }

    protected void Flip(){
        if(body.velocity.x < 0 && facingRight){
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
            Debug.Log("Flipped " + gameObject.name);
        } else if (body.velocity.x > 0 && !facingRight) {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
            Debug.Log("Flipped " + gameObject.name);
        }
    }

    protected bool CheckForGround(){
        if(Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundLayer)){
            noGround = false;
            return true;
        } else {
            noGround = true;
            return false; 
        }
    }

    protected bool CheckForTopWall(){
        if(Physics2D.OverlapCircle(WallCheckTop.position, 0.01f, groundLayer)){
            hitWallBottom = true;
            return true;
        } else {
            hitWallBottom = false;
            return false; 
        }
    }

    protected bool CheckForBottomWall(){
        if(Physics2D.OverlapCircle(WallCheckBottom.position, 0.01f, groundLayer)){
            hitWallTop = true;
            return true;
        } else {
            hitWallTop = false;
            return false; 
        }
    }
    
    protected void Jump(){
        body.velocity = new Vector2(body.velocity.x, 5);
    }

    protected virtual void attackHandler(){

    }

    public virtual void Attack(){

    }
}
