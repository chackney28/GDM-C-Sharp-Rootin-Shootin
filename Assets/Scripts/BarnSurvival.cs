using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class BarnSurvival : NetworkBehaviour
{
    public int iFrames = 0;
    void collisionLogic(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            if (iFrames == 0) takeDamage();
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        collisionLogic(collision);
    }

    void OnCollisionStay2D(Collision2D collision){
        collisionLogic(collision);
    }
    
    void FixedUpdate(){
        if (iFrames > 0) iFrames--;
    }

    void takeDamage(){
         //Calls the GameManager to do the damage
        if (GameManager.Instance.hasPermission){
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.barnHurtSound);
            GameManager.Instance.changeBarnHp(GameManager.Instance.barnHp.Value - 1);
            if (GameManager.Instance.barnHp.Value == 0){ 
                defeat();
            } else {
                iFrames = (NetworkManager.Singleton.ConnectedClientsList.Count > 1) ? 60 : 90;
            }
        }
    }

    void defeat(){
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.barnDeathSound);
        string teamType;
        if (NetworkManager.Singleton.ConnectedClientsList.Count == 2){
            teamType = "Duo";
        } else {
            teamType = "Solo";
        }
        DatabaseManager.Instance.SaveHighScore(teamType, GameManager.Instance.playerWave.Value);
        SceneManager.LoadScene(3);
    }
}
