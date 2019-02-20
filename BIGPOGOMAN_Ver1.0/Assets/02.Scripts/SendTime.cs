using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTime : MonoBehaviour {
    public float time;
    public GameManage gameMng;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void getTime()
    {
        time = gameMng.time;
    }
}
