
using UnityEngine;
using Unity.Netcode;

public class SpawningBehavior : NetworkBehaviour
{
    public static SpawningBehavior Instance { get; private set; }
    [SerializeField] GameObject enemy1;
    [SerializeField] int times;
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
        while(times > 0){
            EnemyWavePool.Instance.SpawnEnemy(enemy1);
            times--;
        }
        //newEnemy.transform.rotation = firePoint.rotation;
    }

    void OnEnable(){
        GameManager.Instance.waveChange += promptMoreWaves;
    }
    
    //Removes aformentioned reactions
    void OnDisable(){
        GameManager.Instance.waveChange -= promptMoreWaves;
    }

    void promptMoreWaves(int waves){
        times = GameManager.Instance.playerWave;
    }
}
