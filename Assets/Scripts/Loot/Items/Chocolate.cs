using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocolate : Item
{
    public override void OnPickUp(GameObject player){
        
        player.GetComponent<PlayerShooting>().attackRate += 0.15f;
        player.GetComponent<PlayerMovement>().movementCoefficient += 0.15f;
    }
}
