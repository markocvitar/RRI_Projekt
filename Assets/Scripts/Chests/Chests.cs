using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chests : MonoBehaviour
{
    [SerializeField] public bool isOpened;
    [SerializeField] private GameObject[] itemPoolCommon;
    [SerializeField] private GameObject[] itemPoolRare;
    [SerializeField] private GameObject[] itemPoolLegendary;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject textMeshPrefab;
    [SerializeField] public GameObject hoveringTextMesh;

    [SerializeField] public int Cost;
    
    [SerializeField] private BoxCollider2D chestCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource chestOpenSound;
    [SerializeField] private AudioSource itemAppearSound;


    [SerializeField] private int chestRarity; //higher = better
    // Start is called before the first frame update
    void Start()
    {
        Cost = (int)((Cost * GameManager.sharedInstance.stage - 1) * 0.75f);
        hoveringTextMesh = Instantiate(textMeshPrefab, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
        hoveringTextMesh.GetComponent<TextMeshPro>().text = Cost + "$";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator openChest(){
        chestOpenSound.Play();
        int GetRandomRarity;
        int GetRandomItem;
        if(!isOpened){
            if(chestRarity == 1){
                GetRandomRarity = Random.Range(1,21);
                switch(GetRandomRarity){
                    case < 12:
                        GetRandomItem = Random.Range(0, itemPoolCommon.Length);
                        isOpened = true;
                        animator.SetBool("isOpened", true);
                        yield return new WaitForSeconds(0.5f);
                        itemAppearSound.Play();
                        Instantiate(itemPoolCommon[GetRandomItem], transform.position, Quaternion.identity);
                        break;
                    case < 20:
                        GetRandomItem = Random.Range(0, itemPoolRare.Length);
                        isOpened = true;
                        animator.SetBool("isOpened", true);
                        yield return new WaitForSeconds(0.5f);
                        itemAppearSound.Play();
                        Instantiate(itemPoolRare[GetRandomItem], transform.position, Quaternion.identity);
                        break;
                    case 20:
                        GetRandomItem = Random.Range(0, itemPoolLegendary.Length);
                        isOpened = true;
                        animator.SetBool("isOpened", true);
                        yield return new WaitForSeconds(0.5f);
                        itemAppearSound.Play();
                        Instantiate(itemPoolLegendary[GetRandomItem], transform.position, Quaternion.identity);
                        break;
                }
            } else if (chestRarity == 2){
                GetRandomRarity = Random.Range(1,21);
                switch(GetRandomRarity){
                    case < 15:
                        GetRandomItem = Random.Range(0, itemPoolRare.Length);
                        isOpened = true;
                        animator.SetBool("isOpened", true);
                        yield return new WaitForSeconds(0.5f);
                        itemAppearSound.Play();
                        Instantiate(itemPoolRare[GetRandomItem], transform.position, Quaternion.identity);
                        break;
                    case >= 15:
                        GetRandomItem = Random.Range(0, itemPoolLegendary.Length);
                        isOpened = true;
                        animator.SetBool("isOpened", true);
                        yield return new WaitForSeconds(0.5f);
                        itemAppearSound.Play();
                        Instantiate(itemPoolLegendary[GetRandomItem], transform.position, Quaternion.identity);
                        break;
                
                }  
            } else if (chestRarity == 3){
                GetRandomItem = Random.Range(0, itemPoolLegendary.Length);
                isOpened = true;
                animator.SetBool("isOpened", true);
                yield return new WaitForSeconds(0.5f);
                itemAppearSound.Play();
                Instantiate(itemPoolLegendary[GetRandomItem], transform.position, Quaternion.identity);
            }
            chestCollider.enabled = false;
            Destroy(hoveringTextMesh);
        }
    }
}
