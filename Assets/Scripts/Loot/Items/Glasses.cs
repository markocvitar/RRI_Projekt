using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Glasses : Item
{
    public override void UpdateItem(GameObject player, int stacks){
        
        GameObject.FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 10f + 2f * stacks;
    }
}
