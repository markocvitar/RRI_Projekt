using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NiCeWeApOn : Item
{
    public override void OnPickUp(GameObject player)
    {
        player.GetComponent<PlayerShooting>().damageMeleeModifier += 1f;
    }
}
