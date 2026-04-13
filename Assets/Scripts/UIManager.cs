using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Sets itself as a manager
    public static UIManager Instance { get; private set; }

    //Gets the UI Elements for Score and Health, do not work when resetting as the specific scene ones don't get reset, 
    //which is probably going to be fixed in a later update/lesson
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    //Sets itself as a manager 2
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        // Persist this GameObject across all scenes
        DontDestroyOnLoad(gameObject);

    }

    //When enabled, checks to see if something from GameManager changed and then reacts accordingly
   void OnEnable(){
        GameManager.Instance.waveChange += updateWaveText;
        //GameManager.Instance.hpChange1 += updateHpText;
        //GameManager.Instance.gamedOver += goToGameover;
    }
    
    //Removes aformentioned reactions
    void OnDisable(){
        GameManager.Instance.waveChange -= updateWaveText;
        //GameManager.Instance.hpChange1 -= updateHpText;
        //GameManager.Instance.gamedOver -= goToGameover;
    }

    //Updates the text to be the accurate score
    public void updateWaveText(int score){
        //Debug.Log("text is scored");
        //Debug.Log(score);
        //scoreText.text = GameManager.Instance.playerWave + " :Wave";
    }

    //Updates the text to be the accurate health value
    public void updateHpText(int hp){
        Debug.Log("text is hp");
        //Debug.Log(hp);
        healthText.text = "Hp: " + GameManager.Instance.player1Hp;
    }

    //When the palyer reaches 0 Hp, go to GameOver
    //(personally would've placed this in GameManager but it was specified in the assignment)
    private void goToGameover(){
        SceneManager.LoadScene("GameOver");
    }
}
