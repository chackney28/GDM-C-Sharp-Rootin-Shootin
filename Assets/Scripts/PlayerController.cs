using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    public int iFrames = 0;
    public int deadTimer = 0;
    public bool lost = false;
    public Vector2 moveInput;
    protected bool isDashing = false;
    private int dashLength = 60;
    private int dashTime = 0;
    private float dashMult = 1.5f;
    private Vector2 dashAngle;
    private bool colorChanged = false;
    [SerializeField] protected internal float baseSpeed = 5f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //Had to do it this way as the game refused to use the normal isServer things and what not. And it does work at regulating
        //who can do what.
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 1){
            GameManager.Instance.hasPermission = true;
        } else {
            if (!GameManager.Instance.gameStarted.Value){
                GameManager.Instance.changeHp2(10);
            } else {
                NetworkManager.Singleton.Shutdown();
            }
        }
    }

    public void movement(InputAction.CallbackContext context){
        moveInput = context.ReadValue<Vector2>();
    }


    public void dashing(InputAction.CallbackContext context){
        //print(StatsManager.boonList);
        //print("dashed");
        if (IsOwner && !GameManager.Instance.playerDead && !GameManager.Instance.isPaused){
        isDashing = true;
        }
    }

    public void pause(InputAction.CallbackContext context)
    {
        GameManager.Instance.startPause();
    }

    public void spawn(InputAction.CallbackContext context){
        if (GameManager.Instance.hasPermission){
            SpawningBehavior.Instance.Spawn();
            GameManager.Instance.gameStarted.Value = true;
        }
    }

    void collisionLogic(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            if (!isDashing && !GameManager.Instance.playerDead && iFrames == 0) takeDamage();
            if (GameManager.Instance.playerDead) collision.gameObject.GetComponent<ZombieBehavior>().obsession = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        collisionLogic(collision);
    }

    void OnCollisionStay2D(Collision2D collision){
        collisionLogic(collision);
    }

    void FixedUpdate()
    {
        if (IsOwner){
        if (!(GameManager.Instance.hasPermission && colorChanged)){ 
            GetComponent<SpriteRenderer>().color = Color.black;
            colorChanged = true;
        }
        if (!GameManager.Instance.playerDead && !GameManager.Instance.isPaused){
            if (!isDashing){
                transform.Translate(new Vector2(moveInput.x, moveInput.y) * Time.deltaTime * baseSpeed);
            } else {
                //print(isDashing);
                //print(dashTime);
                if (dashTime == 0){
                    dashTime = dashLength;
                    dashAngle = new Vector2(moveInput.x, moveInput.y);
                    transform.Translate(dashAngle * baseSpeed * dashMult * Time.deltaTime);
                } else {
                    transform.Translate(dashAngle * baseSpeed * dashMult * Time.deltaTime);
                    dashTime--;
                    if (dashTime == 0) isDashing = false;
                } 
            }
            if (iFrames > 0) iFrames--;
        } else {
            deadTimer--;
            if (deadTimer == 0) revive();
        }
        }
    }

    void takeDamage(){
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.playerHurtSound);
         //Calls the GameManager to do the damage
        if (GameManager.Instance.hasPermission){
            GameManager.Instance.changeHp1(GameManager.Instance.player1Hp.Value - 1);
            if (GameManager.Instance.player1Hp.Value == 0){ 
                die();
            } else {
                iFrames = 40;
            }
        } else {
            GameManager.Instance.changeHp2(GameManager.Instance.player2Hp.Value - 1);
            if (GameManager.Instance.player2Hp.Value == 0){ 
                die();
            } else {
                iFrames = 40;
            }
        }
    }

    void die(){
        GameManager.Instance.playerDead = true;
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.playerDeathSound);
        deadTimer = (NetworkManager.Singleton.ConnectedClientsList.Count > 1) ? 600 : 300;
    }

    void revive(){
        GameManager.Instance.playerDead = false;
        deadTimer = 0;
        if (GameManager.Instance.hasPermission){
            GameManager.Instance.changeHp1(10);
        } else {
            GameManager.Instance.changeHp2(10);
        }
    }
}
