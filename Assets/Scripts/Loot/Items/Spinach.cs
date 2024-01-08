using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinach : Item
{
    public override void OnPickUp(GameObject Player)
    {
        Player.GetComponent<PlayerShooting>().AbilityOneCooldown *= 0.8f;
        Player.GetComponent<PlayerShooting>().AbilityTwoCooldown *= 0.8f;
    }
}
