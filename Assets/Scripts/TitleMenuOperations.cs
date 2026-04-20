using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleMenuOperations : NetworkBehaviour
{
    //The various buttons matched to their respective button on the title screen
    public Button startButton;
    public Button hardButton;
    public Button joinButton;
    public Button scoresButton;
    public Button exitButton;
    //Slider for volume
    public Slider volume;
    void Start(){
        //Assigns buttons their respective purposes
        Button start = startButton.GetComponent<Button>();
        Button hard = hardButton.GetComponent<Button>();
        Button join = joinButton.GetComponent<Button>();
        Button score = scoresButton.GetComponent<Button>();
        Button exit = exitButton.GetComponent<Button>();
		start.onClick.AddListener(startOnClick);
		hard.onClick.AddListener(hardOnClick);
		join.onClick.AddListener(joinOnClick);
		score.onClick.AddListener(scoresOnClick);
		exit.onClick.AddListener(exitOnClick);
        volume.value = PlayerPrefs.GetFloat("volume");
    }

    //Starts first arena
    public void startOnClick(){
        GameManager.Instance.needsSpawned = true;
        SceneManager.LoadScene(1);
	}

    //Starts 2nd arena
    public void hardOnClick(){
        GameManager.Instance.needsSpawned = true;
        SceneManager.LoadScene(2);
	}

    //Join a person currently runing a game
    public void joinOnClick(){
        NetworkManager.Singleton.StartClient();
	}

    //goes to the highest scores page
    public void scoresOnClick(){
        SceneManager.LoadScene(4);
	}

    //Suppose to close the unity window
    public void exitOnClick(){
        Application.Quit();
	}

    //Sets the volume to the slider
    void Update()
    {
       AudioManager.Instance.masterVolume =  volume.value;
    }
}
