using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class AmmoPool : MonoBehaviour
{
    public static AmmoPool Instance { get; private set; }
    
    public GameObject bulletPrefab;
    public Queue<GameObject> activeBullets = new Queue<GameObject>();
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        
        
    }

    public GameObject GetObject()
    {
        if (activeBullets.Count > 0){
            GameObject obj = activeBullets.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return Instantiate(bulletPrefab);
    }
    
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        activeBullets.Enqueue(bullet);
    }
}
