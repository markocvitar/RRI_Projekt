using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadWifi : Item
{
    public override void OnPickUp(GameObject player){
        player.GetComponent<PlayerShooting>().attackRate += 0.15f;
    }
}
