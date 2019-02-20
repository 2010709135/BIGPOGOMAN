using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour {
    public static SteamAchievements script;

    bool unlockTest;
    bool m_bInitialized;

    private void Awake()
    {
        script = this;
        if (!SteamManager.Initialized)
        {
            gameObject.SetActive(false);
            return;
        }
        
    }

    public void LockSteamAchievement(string ID)
    {
        //SteamUserStats.reset
    }

    public void UnlockSteamAchievement(string ID)
    {
        TestSteamAchievement(ID);
        if (!unlockTest)
        {
            Debug.Log("Unlock" + ID);
            SteamUserStats.SetAchievement(ID);
            SteamUserStats.StoreStats();
        }
        else
        {
            Debug.Log("Already Unlock" + ID);
        }
    }

    public void TestSteamAchievement(string ID)
    {
        SteamUserStats.GetAchievement(ID, out unlockTest);

    }

    public void DEBUG_LockSteamAchievement(string ID)
    {
        TestSteamAchievement(ID);
        if (unlockTest)
        {
            SteamUserStats.ClearAchievement(ID);
        }
    }

    public bool GetSteamAchievementStatus(string ID)
    {
        TestSteamAchievement(ID);
        return unlockTest;
    }




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
}
