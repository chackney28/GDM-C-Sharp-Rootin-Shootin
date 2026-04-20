using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class AmmoPool : MonoBehaviour
{
    public static AmmoPool Instance { get; private set; }
    
    public GameObject bulletPrefab;
    public List<GameObject> activeBullets = new List<GameObject>();
    
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
        GameObject newBullet = Instantiate(bulletPrefab);
        activeBullets.Add(newBullet);
        return newBullet;
    }
    
    public void ReturnBullet(GameObject bullet)
    {
        activeBullets.Remove(bullet);
        Destroy(bullet);
    }
}
