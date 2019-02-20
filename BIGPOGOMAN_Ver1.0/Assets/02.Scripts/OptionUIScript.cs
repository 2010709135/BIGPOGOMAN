using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionUIScript : MonoBehaviour {
    public Slider BGM_slider;
    public Slider EffectSound_slider;
    public Slider MouseSens_slider;
    public Toggle Windowed_Toggle;
    public Dropdown QualityDropdown;
    public Dropdown Resolution_List;
    public GameManage GameManager;

    PlayMakerFSM GameManager_Playmaker;

    Resolution[] resolutions;
    int currentResolutionIdx;

    int QualityIdx;

	// Use this for initialization
	void Start () {
        MouseSens_slider.value = GameManager.mouseX;
        BGM_slider.value = GameManager.BGM;
        EffectSound_slider.value = GameManager.EffectSound;

        QualityDropdown.value = QualitySettings.GetQualityLevel();
        GameManager.QualityIdx = QualityDropdown.value;

        resolutions = Screen.resolutions;
        Debug.Log(resolutions.Length);

        Resolution_List.ClearOptions();

        List<string> options = new List<string>();
        currentResolutionIdx = GameManager.GetCurrentResolutionIdx();
        Windowed_Toggle.isOn = GameManager.GetIsFullScreen() ;

        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
        }
        Resolution_List.AddOptions(options);
        Resolution_List.value = currentResolutionIdx;
        Resolution_List.RefreshShownValue();
	}

    public void SetMouseSensChanged()
    {
        Debug.Log(MouseSens_slider.value);
        
        GameManager.setMouseX(MouseSens_slider.value);
    }
	
    public void SetBGMChanged()
    {
        GameManager.setBGM(BGM_slider.value);
    }

    public void SetEffectSound()
    {
        GameManager.setEffectSound(EffectSound_slider.value);
    }

    public void SelectResolution(int resolutionIdx)
    {
        Debug.Log("SelectResolution Called");
        GameManager.SetCurrentResolutionIdx(resolutionIdx);

        Resolution_List.RefreshShownValue();

        Resolution resolution = resolutions[resolutionIdx];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        currentResolutionIdx = resolutionIdx;
    }

    public void ToggleWindowed(bool isFullScreen)
    {
        GameManager.SetIsFullScreen(isFullScreen);
        Screen.SetResolution(resolutions[currentResolutionIdx].width, resolutions[currentResolutionIdx].height, !isFullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        GameManager.QualityIdx = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);

    }

    // Update is called once per frame
    void Update () {
		
	}
}
