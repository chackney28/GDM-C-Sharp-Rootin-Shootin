using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Netcode;

public class PlayerShooting : PlayerController
{
    //Used so the player can't cheese the came by click spamming
    private int shotDelay = 0;
    //Object reference to the Bullet prefab
    public GameObject bullet;
    //Tracks where the mouse is located
    public Vector3 mouseLocation;
    //Gets the place where the bullets are spawning from
    public Transform firePoint;

    public void shoot(InputAction.CallbackContext context){
        if (IsOwner){
        if (shotDelay == 0 && !GameManager.Instance.playerDead){
            GameObject bullet = AmmoPool.Instance.GetObject();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            Rigidbody2D bulrb = bullet.GetComponent<Rigidbody2D>();
            float thrust = 5f;
            bulrb.AddForce(firePoint.right * thrust, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.shotSound);
            shotDelay = 20;
        }
        }
    }

    void Update()
    {
        mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (IsOwner){
        if (!GameManager.Instance.playerDead && !GameManager.Instance.isPaused){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 aimDirect = new Vector2(mouseLocation.x, mouseLocation.y) - rb.position;
        float aimAngle = Mathf.Atan2(aimDirect.y, aimDirect.x) * Mathf.Rad2Deg;// - 90f;
        rb.rotation = aimAngle;

        if (shotDelay > 0){
            shotDelay--;
        }
        }
        }
    }
}