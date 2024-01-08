using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : Item
{
    // Start is called before the first frame update
    public override void UpdateItem(GameObject player, int stacks)
    {
        player.GetComponent<PlayerHealth>().damageReduction = (0.15f * stacks)/(0.15f * stacks + 1);
    }
}
