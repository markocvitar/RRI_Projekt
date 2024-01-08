using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicePairOfSneakers : Item
{    
    public override void OnPickUp(GameObject player)
    {
        player.GetComponent<PlayerMovement>().moveSpeed += 15f;
        player.GetComponent<PlayerMovement>().canTakeFallDamage = false;
        player.GetComponent<PlayerMovement>().jumpForce += 3f;
    }
}
