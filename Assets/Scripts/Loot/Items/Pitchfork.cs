using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitchfork : Item
{
    public override void OnPickUp(GameObject player)
    {
        player.GetComponent<PlayerShooting>().damageRangedModifier += 0.05f;
        player.GetComponent<PlayerShooting>().damageMeleeModifier += 0.05f;
    }
}
