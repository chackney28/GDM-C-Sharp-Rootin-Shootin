using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Sets it as a manager
    public static GameManager Instance { get; private set; }

    //Various checks for when thigns happen
    public event Action<int> waveChange;
    public event Action<int> hpChange1;
    public event Action<int> hpChange2;
    //public event Action gamedOver;
    
    //Variables for the player
    public int player1Hp = 10;
    public int player2Hp = 10;
    public int playerWave = 1;
    public bool hasPermission = false;

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
        player1Hp -= 1;
        if (player1Hp <= 0){
            player1Hp = 0;
            //gamedOver?.Invoke();
        }
        hpChange1?.Invoke(player1Hp);
    }

     //A function used to change the hp within the manager
    public void changeHp2(int hit){
        //print("Hit");
        player2Hp -= 1;
        if (player2Hp <= 0){
            player2Hp = 0;
            //gamedOver?.Invoke();
        }
        hpChange2?.Invoke(player2Hp);
    }

    //A function used to change the hp within the manager
    public void changeWave(int wave){
        //print("Hit");
        playerWave++;
        waveChange?.Invoke(playerWave);
    }
}
