using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData{    
    public float time; // time for playing BIGPOGOMAN
    [SerializeField]
    // position when player exit game
    public float pos_x;
    public float pos_y;
    public float pos_z;
    // no quaternion or rotation vector
    // start with (0,0,0) rotation

    // for option setting
    public float mouseX;
    public float mouseY;

    public float BGM;
    public float EffectSound;

    // for display option setting
    public int ResolutionIdx;
    public bool isFullScreen;
    public int QualityIdx;

    public GameData(float time, 
        float pos_x, float pos_y, float pos_z,
        float mouseX, float mouseY, 
        float BGM, float EffectSound, 
        int ResolutionIdx, bool isFullScreen, int QualityIdx)
    {
        this.time = time;

        this.pos_x = pos_x;
        this.pos_y = pos_y;
        this.pos_z = pos_z;

        this.mouseX = mouseX;
        this.mouseY = mouseY;

        this.BGM = BGM;
        this.EffectSound = EffectSound;

        this.ResolutionIdx = ResolutionIdx;
        this.isFullScreen = isFullScreen;

        this.QualityIdx = QualityIdx;
    }
    
}

