using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using TMPro;

public class SubmitButton : NetworkBehaviour
{
    public Button resumeButton;
    public Button exitButton;
    public Slider volume;

    //Gets the Button Components and assigns them to their respective tasks
    void Start(){
        Button resume = resumeButton.GetComponent<Button>();
        Button exit = exitButton.GetComponent<Button>();
		resume.onClick.AddListener(TaskOnClick);
		exit.onClick.AddListener(TaskOnClick2);
    }

    //Resumes the encounter
    public void TaskOnClick(){
        GameManager.Instance.endPause();
        gameObject.SetActive(false);
	}

    //Quits the match
    public void TaskOnClick2(){
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene(0);
	}

    //Adjusts the slider to the volume
    void Update()
    {
       AudioManager.Instance.masterVolume =  volume.value;
    }
}
