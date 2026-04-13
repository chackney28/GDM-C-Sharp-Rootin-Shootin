
using UnityEngine;
using Unity.Netcode;

public class ZombieBehavior : NetworkBehaviour
{

    public int hp = 10;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate(){
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.5f * Time.deltaTime);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public void takeDamage(){
        hp--;
        if (hp <= 0) EnemyWavePool.Instance.KillEnemy(gameObject);
    }
}
