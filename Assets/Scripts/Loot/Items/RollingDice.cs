using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : Item
{

    public override void OnPickUp(GameObject Player)
    {
        int statChosen = Random.Range(1, 5);
        int statAmount = Random.Range(1,7);
        switch(statChosen){
            case 1:
                Player.GetComponent<PlayerShooting>().attackRate += Player.GetComponent<PlayerShooting>().baseAttackRate * 0.1f;
                break;
            case 2:
                Player.GetComponent<PlayerMovement>().jumpForce += statAmount;
                break;
            case 3:
                Player.GetComponent<PlayerMovement>().moveSpeed += statAmount;
                break;
            case 4:
                Player.GetComponent<PlayerShooting>().damageMeleeModifier += statAmount * 0.1f;
                Player.GetComponent<PlayerShooting>().damageRangedModifier += statAmount * 0.1f;
                break;
        }
    }

}
