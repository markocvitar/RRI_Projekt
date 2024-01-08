using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string ItemName;
    public int rarity;    
    public string description;
    public GameObject textMeshPrefab;
    public GameObject hoveringTextMesh;

    void Start(){
        DontDestroyOnLoad(gameObject);
        hoveringTextMesh = Instantiate(textMeshPrefab, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
        hoveringTextMesh.GetComponent<TextMeshPro>().text = ItemName + " - " + description;
    }
    
    public virtual void UpdateItem(GameObject player, int stacks){ }
    public virtual void OnHit(PlayerInventory playerInventory, GameObject enemy, int stacks){ }
    public virtual void OnDeath(GameObject gameObject, int stacks){ }
    public virtual void OnPickUp(GameObject player){ }
    public virtual void OnTakeDamage(GameObject player, GameObject enemy, int damage, int stacks){ }
    public virtual void OnNewStage(GameObject player, int stacks){ }

}
