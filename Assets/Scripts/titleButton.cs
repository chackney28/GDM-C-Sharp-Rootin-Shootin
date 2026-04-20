using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleButton : NetworkBehaviour
{
    public Button titleButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        Button title = titleButton.GetComponent<Button>();
		title.onClick.AddListener(startOnClick);
    }

    //Goes to the first real level, resets things back to the basics incase of using the reset button
    public void startOnClick(){
        SceneManager.LoadScene(0);
	}
}
