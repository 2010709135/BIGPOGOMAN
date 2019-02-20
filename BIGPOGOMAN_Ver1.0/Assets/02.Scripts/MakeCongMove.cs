using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCongMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removeEveryRigibody(GameObject CongRoot)
    {
        CongRoot.GetComponent<Rigidbody>().isKinematic = true;
        CongRoot.GetComponent<Rigidbody>().useGravity = false;
    }
    
}
