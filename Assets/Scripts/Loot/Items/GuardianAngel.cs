using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAngel : Item
{
    public override void OnDeath(GameObject player, int stacks)
    {
        if(stacks > 0){
            player.GetComponent<PlayerHealth>().Heal((int)player.GetComponent<PlayerHealth>().MaxHealth, stacks);
            foreach(ItemList item in player.GetComponent<PlayerInventory>().items){
                if(item.item.ItemName == "GuardianAngel"){
                    item.stacks--;
                    return;
                }
            }
        }       
    }
}
