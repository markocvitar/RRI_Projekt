using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager sharedInstance = null;
    public GameObject playerPicked;
    [SerializeField] public GameObject Player;
    [SerializeField] public PlayerInventory playerInventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private GameObject Portal;
    [SerializeField] private Animator portalAnimator;
    [SerializeField] private GameObject PortalEffectBlue;
    [SerializeField] private GameObject PortalEffectYellow;
    [SerializeField] private GameObject PortalEffectRed;

    public bool isLooping = false;
    public bool gameEnded = false;
    private int coinValue = 0;
    public int stage;
    public float eventTime = 120f;
    public float timer;

    public bool eventHappening = false;
    public bool teleportEventDone = false;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI heartText; 
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private EnemySpawnHandler enemySpawnHandler;



    void Awake(){
        stage = 0;
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            sharedInstance = this;
        }
    }

    void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("Scene loaded:" + scene.name);
        try{
            goldText = GameObject.Find("Gold").GetComponent<TextMeshProUGUI>();
            heartText = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
            eventText = GameObject.Find("EventText").GetComponent<TextMeshProUGUI>();
            stageText = GameObject.Find("StageText").GetComponent<TextMeshProUGUI>();
            timer = eventTime;
            InvokeRepeating("UpdateUI", 0f, 0.1f);
            Portal = GameObject.FindGameObjectWithTag("Portal");
            portalAnimator = Portal.GetComponent<Animator>();
            foreach(ItemList item in playerInventory.items){
                item.item.OnNewStage(Player, item.stacks);     
            }
        } catch (NullReferenceException e){
            Debug.Log("No GameUI yet");
        }
    }
        
    
    public void LoadStageOne(){
        if(Player == null){
            Player = Instantiate(playerPicked);
            playerInventory = Player.GetComponent<PlayerInventory>(); 
            playerHealth = Player.GetComponent<PlayerHealth>();
            playerShooting = Player.GetComponent<PlayerShooting>();
        }
        stage += 1;
        SceneManager.LoadScene(2);
        isLooping = false;
        Player.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
        gameEnded = false;
    }

    public void LoadStageTwo(){
        SceneManager.LoadScene(3);
        stage += 1;
        teleportEventDone = false;
        Player.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
    }

    public void LoadStageThree(){
        SceneManager.LoadScene(4);
        stage += 1;
        teleportEventDone = false;
        Player.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
    }

    public void LoadFinalStage(){
        SceneManager.LoadScene(5);
        stage += 1;
        teleportEventDone = false;
        Player.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(0);
        foreach(GameObject item in GameObject.FindGameObjectsWithTag("Item")){
            Destroy(item);
        }
    }

    public void LoadCharacterSelect(){
        SceneManager.LoadScene(1);
    }

    public void LoadAboutScene(){
        SceneManager.LoadScene(7);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void LoadGameOver(){
        SceneManager.LoadScene(6);
        stage = 0;
        Destroy(Player);
    }

    private void UpdateUI(){
        if (goldText != null && heartText != null)
        {
            goldText.text = "Gold " + playerInventory.Gold.ToString();
            heartText.text = "Health " + playerHealth.Health.ToString() + "/" + playerHealth.MaxHealth.ToString();
            stageText.text = "Stage " + (stage - 1);
        }
    }

    public void teleportEvent(){
        if(!eventHappening && !teleportEventDone){
            InvokeRepeating("Countdown", 0f, 1f);
        }
    }

    public void Countdown(){
        timer -= 1;
        eventHappening = true;
        if(timer < 0){
            portalAnimator.SetInteger("State", SceneManager.GetActiveScene().buildIndex - 1);
            teleportEventDone = true;
            eventHappening = false;
            eventText.text = "Teleporting to new stage.";
            CancelInvoke("Countdown");
            switch(SceneManager.GetActiveScene().buildIndex){
            case 2:
                Invoke("LoadStageTwo", 2f);
                Player.SetActive(false);
                Instantiate(PortalEffectBlue, Player.transform.position, Quaternion.identity);
                break;
            case 3:
                Invoke("LoadStageThree", 2f);
                Player.SetActive(false);
                Instantiate(PortalEffectBlue, Player.transform.position, Quaternion.identity);
                break;
            case 4:
                if(!isLooping){
                    Invoke("LoadFinalStage", 2f);
                } else {
                    Invoke("LoadStageOne", 2f);
                }
                Player.SetActive(false);
                Instantiate(PortalEffectBlue, Player.transform.position, Quaternion.identity);
                break;
            }

        }
        if(eventHappening){
            eventText.text = "Survive...<br>" + timer;
        }

    }

    public int GetCurrentScene(){
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void ResetTimer(){
        timer = eventTime; 
    }
}
