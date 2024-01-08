using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] public PlayerInventory playerInventory;
    [SerializeField] private Collider2D hitInfo;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private AudioSource interactSound;
    
    void Update()
    {
        interactionHandler();
    }

    public void interactionHandler(){
        if(Input.GetKeyDown(KeyCode.F)){
            try{
                hitInfo = Physics2D.OverlapCircle(transform.position, 0.3f, interactableLayer);
                switch(hitInfo.tag){
                    case "Item":
                        pickUpItem(hitInfo);
                        break;
                    case "Chest":
                        if(playerInventory.Gold >= hitInfo.gameObject.GetComponent<Chests>().Cost){
                            StartCoroutine(hitInfo.GetComponent<Chests>().openChest());
                            playerInventory.Gold -= hitInfo.gameObject.GetComponent<Chests>().Cost;
                        }
                        break;
                    case "Portal":
                        if(!GameManager.sharedInstance.eventHappening && !GameManager.sharedInstance.teleportEventDone){
                            if(GameManager.sharedInstance.GetCurrentScene() == 4){
                                Invoke("CheckForLoop", 1f);
                                GameManager.sharedInstance.teleportEvent();
                            } else GameManager.sharedInstance.teleportEvent();
                        }
                        break;
                } 
            } catch (NullReferenceException e){
                Debug.Log("No interactable found");
            }
        }
    }

    public void pickUpItem(Collider2D item){
        item.GetComponent<Item>().hoveringTextMesh.SetActive(false);
        interactSound.Play();
        item.transform.SetParent(gameObject.transform);
        foreach(ItemList i in playerInventory.items){
            if(i.item.ItemName == item.GetComponent<Item>().ItemName){
                i.item.OnPickUp(playerInventory.gameObject);
                i.stacks++;
                item.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                item.enabled = false;
                return;
            }
        }
        playerInventory.items.Add(new ItemList(item.GetComponent<Item>(), 1));
        item.GetComponent<Item>().OnPickUp(playerInventory.gameObject);
        item.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        item.enabled = false;
    }

    private void CheckForLoop(){
        if(Input.GetKey(KeyCode.F) || Physics2D.OverlapCircle(transform.position, 0.3f, interactableLayer).CompareTag("Portal")){
            GameManager.sharedInstance.isLooping = true;
        }
    }
}
