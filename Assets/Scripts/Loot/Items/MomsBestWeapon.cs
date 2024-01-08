using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsBestWeapon : Item
{
    // Start is called before the first frame update
    public override void OnPickUp(GameObject player)
    {
        player.GetComponent<PlayerShooting>().damageRangedModifier += 1f;
    }
}
