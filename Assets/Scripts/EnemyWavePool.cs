using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class EnemyWavePool : NetworkBehaviour
{
    public static EnemyWavePool Instance { get; private set; }
    public List<GameObject> activeEnemies = new List<GameObject>();
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        if (GameManager.Instance.hasPermission ){
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        activeEnemies.Add(newEnemy);
        var instanceNetworkObject = newEnemy.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn(true);
        }
    }

    public void KillEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        if (IsServer && IsSpawned) {
            enemy.GetComponent<NetworkObject>().Despawn();
        }
        Destroy(enemy);
        if (activeEnemies.Count == 0){
            GameManager.Instance.changeWave(1);
            SpawningBehavior.Instance.Spawn();
        }
    }
}
