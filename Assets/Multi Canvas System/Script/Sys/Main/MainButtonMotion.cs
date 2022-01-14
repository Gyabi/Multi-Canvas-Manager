using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtonMotion : MonoBehaviour
{
  
    public void OnClickMainSettingButton()
    {
        CanvasManager.Instance.SwitchSettingShow();
    }
}
