using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Item{
    // Start is called before the first frame update
    public override void OnPickUp(GameObject player){
        player.GetComponent<PlayerMovement>().movementCoefficient += 0.15f;
    }
}
