
using UnityEngine;
using Unity.Netcode;

public class ZombieBehavior : NetworkBehaviour
{
    //Makes sure the Networks tracks these for proper use
    public NetworkVariable<int> zomebieHp = new NetworkVariable<int>(3);
    public NetworkVariable<bool> zomebieDead = new NetworkVariable<bool>(false);
    //Variables in relation to the chase mechanics
    public GameObject barn;
    public GameObject obsession;

    //Zombie will B-Line it to the barn once it spawns
    void Start()
    {
        obsession = barn;
    }

    void FixedUpdate(){
        //Checks if it is dead, so that the second player can kill them with 0 worries
        if (zomebieDead.Value && GameManager.Instance.hasPermission) EnemyWavePool.Instance.KillEnemy(gameObject);
        //If the player has left their chase range, they refocus onto the barn.
        if (obsession == null) obsession = barn;
        transform.position = Vector3.MoveTowards(transform.position, obsession.transform.position, 1.5f * Time.deltaTime);
    }

    //The trigger is the way it can see if the player is near them or not.
    private void OnTriggerEnter2D(Collider2D thing){
        if (thing.gameObject.CompareTag("Player")){
            obsession = thing.gameObject;
        }
    }
    
    //The trigger is the way it can see if the player is near them or not
    private void OnTriggerExit2D(Collider2D thing){
        if (thing.gameObject.CompareTag("Player")){
            obsession = null;
        }
    }

    //Process what happens when they are shot with a bullet
    public void takeDamage(){
        setZombieHpServerRpc(zomebieHp.Value - 1);
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.enemyHurtSound);
        if (zomebieHp.Value <= 0) setZombieDeadServerRpc(true);
    }

    //Sends info to the server that they have had their hp changed
    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void setZombieHpServerRpc(int value){
        zomebieHp.Value = value;
    }

    //Sends to the server that they are dead
    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void setZombieDeadServerRpc(bool value){
        zomebieDead.Value = value;
    }
}
