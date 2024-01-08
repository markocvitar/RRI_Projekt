using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;

    [Header("Base class")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;
    [SerializeField] protected bool isGrounded;
    [SerializeField] public bool facingRight;
    [SerializeField] public bool canTakeFallDamage = true;
    [SerializeField] public float movementCoefficient = 1.0f;
    [SerializeField] protected float fallDamageThreshold;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected GameObject groundCheck;
    [SerializeField] protected Animator playerAnimation;
    [SerializeField] protected PlayerShooting playerShooting;
    protected int maxYvelocity = 0;

    protected enum AnimationState{
        idle = 0,
        running = 1,
        jumping = 2,
        falling = 3

    }

    void Start() {
        body = GetComponent<Rigidbody2D>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    protected void Jump(){
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

    public virtual void movementHandler() {
        
        checkIfGrounded();

        float dirX = Input.GetAxis("Horizontal");
        flipPlayer(dirX);


        /* 
        Debug.Log("Velocity y: " + body.velocity.y);
        Debug.Log("Is grounded: " + isGrounded); 
        */
        
        if(movementCoefficient < 0){
            body.velocity = new Vector2(dirX * (moveSpeed * 0), body.velocity.y);        
        } else {
            body.velocity = new Vector2(dirX * (moveSpeed * movementCoefficient), body.velocity.y);
        }
        
        if(Input.GetKey(KeyCode.Space) && isGrounded){
            Jump();
        }
        if (body.velocity.y < maxYvelocity){
            maxYvelocity = (int)body.velocity.y;
        }
    }

    public virtual void flipPlayer(float dirX){
        if(dirX < 0 && facingRight){
            facingRight = !facingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            playerShooting.attackPoint.transform.Rotate(0f, 180f, 0f);
            transform.localScale = scale;
            facingRight = false;
        } else if (dirX > 0 && !facingRight) {
            facingRight = !facingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            playerShooting.attackPoint.transform.Rotate(0f, 180f, 0f);
            transform.localScale = scale;
            facingRight = true;
        }
    }

    public virtual bool checkIfGrounded(){
        if (Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(0.45f, 0.1f), 0, groundLayer) == true || Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(0.45f, 0.1f), 0, enemyLayer) == true ){
            isGrounded = true;
            return true;
        } else {
            isGrounded = false;
            return false;
        }
    }

    protected void checkAnimation(){
        if((body.velocity.x > 0.5 || body.velocity.x < -0.5) && isGrounded) {
            playerAnimation.SetInteger("State", (int)AnimationState.running);
        } else if (body.velocity.y > 0.01) {
            playerAnimation.SetInteger("State", (int)AnimationState.jumping);
        } else if (body.velocity.y < -0.01) {
            playerAnimation.SetInteger("State", (int)AnimationState.falling);
        } else {
            playerAnimation.SetInteger("State", (int)AnimationState.idle);
        }
    }

    protected void checkFallDamage(){
        if(canTakeFallDamage && maxYvelocity < -fallDamageThreshold && isGrounded){
            Debug.Log("Took " + (-maxYvelocity) + " damage");
            gameObject.GetComponent<PlayerHealth>().TakeDamage(Mathf.Abs(maxYvelocity), null);
            maxYvelocity = 0;
        }
    }
}