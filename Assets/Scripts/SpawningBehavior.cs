
using UnityEngine;
using Unity.Netcode;

public class SpawningBehavior : NetworkBehaviour
{
    public static SpawningBehavior Instance { get; private set; }
    //Enemy1 does imply there was meant to be more enemies, unfortunately this ended not being the case
    [SerializeField] GameObject enemy1;
    //Number of times to loop the thing to spawn more and more zombies
    [SerializeField] int times;
    //Hides the starting text upon spawning an enemy
    public GameObject startText;
    // Update is called once per frame
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

    public void Spawn()
    {
        startText.SetActive(false);
        while(times > 0){
            EnemyWavePool.Instance.SpawnEnemy(enemy1);
            times--;
        }
    }

    //Checks the waveChange in GameManager and if something has then it spawns another wave of enemies
    void OnEnable(){
        GameManager.Instance.waveChange += promptMoreWaves;
    }
    
    //Removes aformentioned reactions
    void OnDisable(){
        GameManager.Instance.waveChange -= promptMoreWaves;
    }

    //Actualy process of spawning another wave
    void promptMoreWaves(int waves){
        times = GameManager.Instance.playerWave.Value;
        if (GameManager.Instance.hasPermission) Spawn();
    }
}
