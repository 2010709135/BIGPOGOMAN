using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressContinue()
    {

    }

    public void PressNewGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void PressHowToPlay()
    {

    }

    public void PressOption()
    {

    }

    public void PressQuit()
    {
        Application.Quit();
    }
}
