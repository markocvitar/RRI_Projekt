using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broccolli : Item
{

    public override void OnPickUp(GameObject Player)
    {
        Player.GetComponent<PlayerHealth>().MaxHealth += 25f;
    }
}
