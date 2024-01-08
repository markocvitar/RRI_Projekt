using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassOneMovement : PlayerMovement
{
    [Header("Class 1")]
    [SerializeField] private bool usedWallJump;
    [SerializeField] private GameObject wallCheck;    
    [SerializeField] private ClassOneShooting class1Shooting;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    private void FixedUpdate() {
        if(class1Shooting.isUsingAbilityOne){
            return;
        } else if(class1Shooting.isUsingAbilityTwo){
            body.velocity = new Vector2(0, 0);
            return;
        }
        movementHandler();
        checkFallDamage();
        checkAnimation();
    }

    public override void movementHandler() {
        
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
        } else if (Input.GetKey(KeyCode.Space) && (checkForWall() && !usedWallJump)){
            usedWallJump = true;
            Jump();
        }
        if (body.velocity.y < maxYvelocity){
            maxYvelocity = (int)body.velocity.y;
        }
    }

    public override bool checkIfGrounded(){
        if (Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(0.45f, 0.1f), 0, groundLayer) == true || Physics2D.OverlapBox(groundCheck.transform.position, new Vector2(0.45f, 0.1f), 0, enemyLayer) == true ){
            usedWallJump = false;
            isGrounded = true;
            return true;
        } else {
            isGrounded = false;
            return false;
        }
    }

    private bool checkForWall(){
        if(Physics2D.OverlapBox(wallCheck.transform.position, new Vector2(0.3f, 2f), 0, groundLayer) == true){
            return true;
        } else {
            return false;
        }
    }
}