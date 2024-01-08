using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : MonoBehaviour
{
    public List<ItemList> items = new List<ItemList>();
    public int Gold;
    
    void Start() {
        StartCoroutine("CallItemUpdate");
    }
    void Update() {
        
    }

    public IEnumerator CallItemUpdate(){
        foreach(ItemList i in items){
            i.item.UpdateItem(gameObject, i.stacks);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("CallItemUpdate");
    }

    public void CallOnHit(GameObject enemy){
        foreach(ItemList i in items){
            i.item.OnHit(this, enemy, i.stacks);
        }
    }
}
