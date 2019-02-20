using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSongs : MonoBehaviour {
    private static EndingSongs script;
   // public static EndingSongs script;
    // Use this for initialization

    private void Awake()
    {
        if(script == null)
        {
            script = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
