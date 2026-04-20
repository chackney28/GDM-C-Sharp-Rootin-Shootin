//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletBehavior : MonoBehaviour
{
    public GameObject player;
    public Vector3 mouseLocation;
    public bool fired = false;
    private Rigidbody2D rb;
    // Update is called once per frame

    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Increases score and destroys coin
        if (collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<ZombieBehavior>().takeDamage();
        }
        AmmoPool.Instance.ReturnBullet(gameObject);
    }

    
}
