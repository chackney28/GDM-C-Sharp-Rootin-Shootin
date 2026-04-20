using System;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    //Sets it as a manager
    public static GameManager Instance { get; private set; }

    //Various checks for when thigns happen
    public event Action<int> waveChange;
    public event Action<int> hpChange1;
    public event Action<int> hpChange2;
    public event Action<int> barnHpChange;
    //public event Action gamedOver;
    
    //Variables for the player
    public NetworkVariable<int> player1Hp = new NetworkVariable<int>(10);
    public NetworkVariable<int> player2Hp =  new NetworkVariable<int>(-1);
    public NetworkVariable<int> barnHp = new NetworkVariable<int>(30);
    public NetworkVariable<int> playerWave = new NetworkVariable<int>(1);
    public NetworkVariable<bool> gameStarted = new NetworkVariable<bool>(false);
    public bool hasPermission = false;
    public bool playerDead = false;
    public bool isPaused = false;
    public bool needsSpawned = false;
    public GameObject pauseMenu;

    //Sets it as manager 2
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        // Persist this GameObject across all scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //A function used to change the hp within the manager
    public void changeHp1(int hit){
        //print("Hit");
        player1Hp.Value = hit;
        if (player1Hp.Value <= 0){
            player1Hp.Value = 0;
        }
        hpChange1?.Invoke(player1Hp.Value);
    }

     //A function used to change the hp within the manager
    public void changeHp2(int hit){
        //print("Hit");
        player2Hp.Value = hit;
        if (player2Hp.Value <= 0){
            player2Hp.Value = 0;
        }
        hpChange2?.Invoke(player2Hp.Value);
    }

    public void changeBarnHp(int hit){
        //print("Hit");
        barnHp.Value = hit;
        if (barnHp.Value <= 0){
            barnHp.Value = 0;
        }
        barnHpChange?.Invoke(barnHp.Value);
    }

    //A function used to change the hp within the manager
    public void changeWave(int wave){
        //print("Hit");
        playerWave.Value++;
        waveChange?.Invoke(playerWave.Value);
    }

    public void startPause(){
        pauseMenu.SetActive(true);
        isPaused = true;
    }

    public void endPause(){
        pauseMenu.SetActive(false);
        isPaused = false;
    }
}
