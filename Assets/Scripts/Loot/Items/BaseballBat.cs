using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : Item
{
    public override void UpdateItem(GameObject Player, int stacks)
    {
        Player.GetComponent<PlayerShooting>().critChance = 10f * stacks;
    }
}
