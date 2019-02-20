using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HutongGames.PlayMaker;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {
    public GameObject CongContinuePos;
    public GameObject CongNewStartPos;
    public GameObject CongPrefab;
    public GameObject CongRoot;
    
    Transform CongContinue_tr;

    public bool StartFirst;

    public float mouseX;
    public float mouseY;

    public float time;

    public float BGM;
    public float EffectSound;

    public Button Continue_Btn;

    int CurrentResolutionIdx;
    bool isFullScreen;

    public int QualityIdx;

    public bool end = false;



	// Use this for initialization
	void Start () {
        mouseX = 1.5f;  mouseY = 1.5f;
        CongContinue_tr = CongContinuePos.GetComponent<Transform>();
        StartFirst = false;

        LoadFile(); 
        
        // After Load data set every option values
        FsmVariables.GlobalVariables.FindFsmFloat("MouseSensitivity_X").Value = mouseX;
        FsmVariables.GlobalVariables.FindFsmFloat("MouseSensitivity_Y").Value = mouseY;
        setBGM(BGM);
        setEffectSound(EffectSound);

        Debug.Log("InStart : " + CongContinue_tr.position);
        CongPrefab.GetComponent<Transform>().position = CongContinue_tr.position;
        CongPrefab.GetComponent<Transform>().rotation = CongContinue_tr.rotation;        
    }

    public float CheckTime()
    {
        return time;
    }

    // Update is called once per frame
    void Update () {
        if (!end)
        {
            time += Time.deltaTime;
            FsmVariables.GlobalVariables.FindFsmFloat("Time").Value = time;
        }
    }

    public void setEnd()
    {
        end = true;
    }

    public void setMouseX(float mouseX)
    {
        this.mouseX = mouseX;
        FsmVariables.GlobalVariables.FindFsmFloat("MouseSensitivity_X").Value = mouseX;

    }

    public void setBGM(float BGM)
    {
        this.BGM = BGM;
        FsmVariables.GlobalVariables.FindFsmFloat("BGM").Value = BGM;
    }

    public void setEffectSound(float EffectSound)
    {
        this.EffectSound = EffectSound;

        FsmVariables.GlobalVariables.FindFsmFloat("EffectSound").Value = EffectSound;
    }

    public bool getStartFirst()
    {
        return StartFirst;
    }

    public void setStartFirstFalse()
    {
        StartFirst = false;
        Continue_Btn.interactable = true;
    }

    private void OnApplicationQuit()
    {
        SaveFile();
        
    }

    public void setNewCong(GameObject Cong_p, GameObject Cong_Root)
    {
        CongPrefab = Cong_p;
        CongRoot = Cong_Root;
    }   

    public void SaveFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";

        FileStream file;
        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        Vector3 tempPos =
            new Vector3(CongRoot.GetComponent<Transform>().position.x,
                        CongRoot.GetComponent<Transform>().position.y,
                        CongRoot.GetComponent<Transform>().position.z);
        Debug.Log("InSave : " + tempPos);
        GameData data = new GameData(time, 
            tempPos.x, tempPos.y, tempPos.z, 
            mouseX, mouseY, BGM, EffectSound, 
            CurrentResolutionIdx, isFullScreen, QualityIdx);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        Resolution[] resolutions;
        resolutions = Screen.resolutions;  // return available resolutions

        if (File.Exists(destination)) // if find save.dat file then read from it
            file = File.OpenRead(destination);
        // if there is no save.dat file, then set options with default value
        else{
            Continue_Btn.interactable = false;
            time = 0f;

            StartFirst = true;
            mouseX = 1.5f;
            mouseY = 1.5f;
            BGM = 1f;
            EffectSound = 1f;
            QualityIdx = 2;

            QualitySettings.SetQualityLevel(QualityIdx);

            Debug.LogError("File not found");
            CongContinue_tr.position = CongNewStartPos.GetComponent<Transform>().position;
            CongContinue_tr.rotation = CongNewStartPos.GetComponent<Transform>().rotation;

            Screen.SetResolution(
                Screen.currentResolution.width, 
                Screen.currentResolution.height, 
                FullScreenMode.FullScreenWindow);            

            CurrentResolutionIdx = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    CurrentResolutionIdx = i;
                }
            }

            isFullScreen = false;


            return;
        }


        // read save.dat file and deserialize data into GameData Type instance
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        // set options with value that came from save.dat file
        time = data.time;        
        Vector3 tempPos = new Vector3(data.pos_x, data.pos_y, data.pos_z);
        Debug.Log("InLoad : " + tempPos);
        Quaternion tempQuat = new Quaternion(0, 0, 0, 0);
        CongContinue_tr.SetPositionAndRotation(tempPos, tempQuat);

        mouseX = data.mouseX;
        mouseY = data.mouseY;

        if (Screen.resolutions.Length - 1 < data.ResolutionIdx)
        {
            CurrentResolutionIdx = Screen.resolutions.Length - 1;
            if(data.isFullScreen)
                Screen.SetResolution(resolutions[CurrentResolutionIdx].width
                , resolutions[CurrentResolutionIdx].height, !data.isFullScreen);
            else
                Screen.SetResolution(resolutions[CurrentResolutionIdx].width
                , resolutions[CurrentResolutionIdx].height, !data.isFullScreen);
        }
        else
        {
            CurrentResolutionIdx = data.ResolutionIdx;
        }
        isFullScreen = data.isFullScreen;

        QualityIdx = data.QualityIdx;
        QualitySettings.SetQualityLevel(QualityIdx);

        BGM = data.BGM;
        EffectSound = data.EffectSound;
    }

    public void SetTimeZero()
    {
        time = 0f;
    }

    public void SetCurrentResolutionIdx(int idx)
    {
        this.CurrentResolutionIdx = idx;
    }
    
    public int GetCurrentResolutionIdx()
    {
        return CurrentResolutionIdx;
    }
    public void SetIsFullScreen(bool isFullScreen)
    {
        this.isFullScreen = isFullScreen;
    }
    public bool GetIsFullScreen()
    {
        return isFullScreen;
    }
}
