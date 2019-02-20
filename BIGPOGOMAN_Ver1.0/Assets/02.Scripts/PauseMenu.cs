using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayMaker;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject OptionMenuUI;
    public GameObject HowToPlayUI;

    public GameObject MouseLookInput;
    public GameObject CongRootObj;
    public GameObject AntiClippingRayCast;

    public GameObject GameManagerObj;

    Vector3 NewStart_Vec3;

    PlayMakerFSM MouseLook_FSM;
    PlayMakerFSM CongRot_FSM;
    PlayMakerFSM MoveToZero_FSM;
    PlayMakerFSM GameManagerObjFSM;
    PlayMakerFSM MouseFSM;

    GameManage GameManager_Script;

    bool OptionOn;
    bool hideCursor;
    bool HowToPlayOn;

    bool first_Anti_to_Zero;

    bool isPaused;

    

    // Use this for initialization
    void Start () {
        Time.timeScale = 0f;
        GameIsPaused = true;
        MouseLook_FSM = PlayMakerFSM.FindFsmOnGameObject(MouseLookInput, "FSM");
        CongRot_FSM = PlayMakerFSM.FindFsmOnGameObject(CongRootObj, "MouseBtnFSM");
        MoveToZero_FSM = PlayMakerFSM.FindFsmOnGameObject(AntiClippingRayCast, "MoveToZeroFSM");

        MouseFSM = PlayMakerFSM.FindFsmOnGameObject(this.gameObject, "MouseFSM");

        GameManagerObjFSM = PlayMakerFSM.FindFsmOnGameObject(GameManagerObj, "FSM");
        GameManager_Script = GameManagerObj.GetComponent<GameManage>();

        first_Anti_to_Zero = true;


        OptionOn = false;
        HowToPlayOn = false;

        isPaused = true;
    }

    public void SetFSMFunc(GameObject MouseLookInput_p, GameObject CongRootObj_p, GameObject AntiClippingRaycast_p)
    {
        MouseLookInput = MouseLookInput_p;
        CongRootObj = CongRootObj_p;
        AntiClippingRayCast = AntiClippingRaycast_p;

        MouseLook_FSM = PlayMakerFSM.FindFsmOnGameObject(MouseLookInput, "FSM");
        CongRot_FSM = PlayMakerFSM.FindFsmOnGameObject(CongRootObj, "MouseBtnFSM");
        MoveToZero_FSM = PlayMakerFSM.FindFsmOnGameObject(AntiClippingRayCast, "MoveToZeroFSM");

        MouseInputActive();
        first_Anti_to_Zero = false;
        MoveToZero_FSM.SendEvent("AntiToZero");
    }

    // Update is called once per frame
    void Update () {

        if (isPaused)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                if (OptionOn)
                {
                    Back();
                }else if (HowToPlayOn)
                {
                    BackFromHowToPlay();
                }
                else
                    Continue();
            }
            else
            {
                Pause();
            }
        }

	}

    public void MouseInputActive()
    {
        MouseLook_FSM.SendEvent("FollowMouse");
        CongRot_FSM.SendEvent("DoRotate");
    }

    public void MouseInputInactive()
    {
        MouseLook_FSM.SendEvent("NotFollowMouse");
        CongRot_FSM.SendEvent("DoNotRotate");
    }

    public void NewStart()
    {
        if (GameManager_Script.getStartFirst())
        {
            GameManager_Script.setStartFirstFalse();
            Continue();
            return;
        }

        GameManager_Script.SetTimeZero();
        if (first_Anti_to_Zero)
        {
            first_Anti_to_Zero = false;
            MoveToZero_FSM.SendEvent("AntiToZero");
        }
        MouseInputActive();

        Cursor.visible = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        isPaused = false;


        GameManagerObjFSM.SendEvent("SetNewStart");
    }

    public void Continue()
    {
        if (first_Anti_to_Zero)
        {
            first_Anti_to_Zero = false;
            MoveToZero_FSM.SendEvent("AntiToZero");
        }
        MouseInputActive();
        Cursor.visible = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        isPaused = false;
    }

    void Pause()
    {
        MouseInputInactive();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.visible = true;

        isPaused = true;

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        //SceneManager.LoadScene("LobbyScene");
    }

    public void Option()
    {
        pauseMenuUI.SetActive(false);
        OptionMenuUI.SetActive(true);
        OptionOn = true;
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        OptionMenuUI.SetActive(false);

        OptionOn = false;
    }

    public void BackFromHowToPlay()
    {
        pauseMenuUI.SetActive(true);
        HowToPlayUI.SetActive(false);

        HowToPlayOn = false;
    }

    public void HowToPlay()
    {
        pauseMenuUI.SetActive(false);
        HowToPlayUI.SetActive(true);

        HowToPlayOn = true;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            if (!OptionOn)
                Pause();
        }
    }

}
