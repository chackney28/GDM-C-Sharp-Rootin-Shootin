using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    

    //private CharacterController controller;
    public int hp = 10;
    public bool lost = false;
    public Vector2 moveInput;
    private bool isDashing = false;
    private int dashLength = 60;
    private int dashTime = 0;
    private float dashMult = 1.5f;
    private int shotDelay = 0;
    private Vector2 dashAngle;
    public GameObject bullet;
    private InputInterperter inputs;
    public Vector3 mouseLocation;
    public Transform firePoint;
    [SerializeField] protected internal float baseSpeed = 5f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputs = GetComponent<InputInterperter>();
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 1){
            GameManager.Instance.hasPermission = true;
            SpawningBehavior.Instance.Spawn();
        }
    }

    public void movement(InputAction.CallbackContext context){

        moveInput = context.ReadValue<Vector2>();
    }


    public void dashing(InputAction.CallbackContext context){
        //print(StatsManager.boonList);
        //print("dashed");
        if (IsOwner){
        isDashing = true;
        }
    }

    public void shoot(InputAction.CallbackContext context){
        if (IsOwner){
        if (shotDelay == 0){
            GameObject bullet = AmmoPool.Instance.GetObject();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            Rigidbody2D bulrb = bullet.GetComponent<Rigidbody2D>();
            float thrust = 5f;
            bulrb.AddForce(firePoint.right * thrust, ForceMode2D.Impulse);

            //Rigidbody rb = bullet.GetComponent<RigidBody2D>();

            StartCoroutine(DeactivateBullet(bullet));
            //GameObject thing = Instantiate(bullet, transform.position, transform.rotation);
            //thing.SetActive(true);
            //thing.velocity = ;
            shotDelay = 20;
        }
        }
    }

    IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        AmmoPool.Instance.ReturnBullet(bullet);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            if (!isDashing) takeDamage();
        }
    }

    void Update()
    {
        mouseLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (IsOwner){
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

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 aimDirect = new Vector2(mouseLocation.x, mouseLocation.y) - rb.position;
        float aimAngle = Mathf.Atan2(aimDirect.y, aimDirect.x) * Mathf.Rad2Deg;// - 90f;
        rb.rotation = aimAngle;

        if (shotDelay > 0){
            shotDelay--;
        }
        }
    }

    void takeDamage(){
         //Calls the GameManager to do the damage
        GameManager.Instance.changeHp1(1);
        if (hp == 0) lost = true; 
    }
}
