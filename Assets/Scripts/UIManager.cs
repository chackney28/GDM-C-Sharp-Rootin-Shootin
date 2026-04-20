using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class UIManager : NetworkBehaviour
{
    //Sets itself as a manager
    public static UIManager Instance { get; private set; }

    //Gets the UI Elements for Score and Health, do not work when resetting as the specific scene ones don't get reset, 
    //which is probably going to be fixed in a later update/lesson
    public TextMeshProUGUI healthText1;
    public TextMeshProUGUI healthText2;
    public TextMeshProUGUI healthText3;
    public TextMeshProUGUI waveText;


    void Update(){
       healthText1.text = "P1 Hp: " + GameManager.Instance.player1Hp.Value;
       healthText2.text = "P2 Hp: " + GameManager.Instance.player2Hp.Value;
       healthText3.text = "Barn Hp: " + GameManager.Instance.barnHp.Value;
       waveText.text = GameManager.Instance.playerWave.Value + " :Waves";
    }
}
