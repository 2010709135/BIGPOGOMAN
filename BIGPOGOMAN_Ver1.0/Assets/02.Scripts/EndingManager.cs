using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using HutongGames.PlayMaker;

public class EndingManager : MonoBehaviour {
    int time;
    int Hour;
    int minute;
    int sec;

    string totalTime_str;

    public Text Time_Text;

	// Use this for initialization
	void Start () {
        Hour = 0;
        minute = 0;
        sec = 0;
        //LoadFile();
        time = (int)(FsmVariables.GlobalVariables.FindFsmFloat("Time").Value);
        Hour = time / 3600;
        time = time - Hour * 3600;
        minute = time / 60;
        time = time - minute * 60;
        sec = time;

        string hour_str;
        if (Hour < 10)
        {
            hour_str = "0" + Hour.ToString();
        }
        else
            hour_str = Hour.ToString();

        string minute_str;
        if (minute < 10)
            minute_str = "0" + minute.ToString();
        else
            minute_str = minute.ToString();

        string sec_str;
        if (sec < 10)
        {
            sec_str = "0" + sec.ToString();
        }
        else
            sec_str = sec.ToString();


        totalTime_str = hour_str + " : " + minute_str + " : " + sec_str;

        Time_Text.text = totalTime_str;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
           

            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        time = (int)(data.time);
        //time = 9895;
        Debug.Log(time);
                
    }

    public void RemoveSaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        File.Delete(destination);
    }

}
