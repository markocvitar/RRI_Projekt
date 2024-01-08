using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject GunGuy;
    [SerializeField] private GameObject Ninja;

    public void pickGunGuy(){
        GameManager.sharedInstance.playerPicked = GunGuy;
        GameManager.sharedInstance.LoadStageOne();
    }

    public void pickNinja(){
        GameManager.sharedInstance.playerPicked = Ninja;
        GameManager.sharedInstance.LoadStageOne();
    }
}
