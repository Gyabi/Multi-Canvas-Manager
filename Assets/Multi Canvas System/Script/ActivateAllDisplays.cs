using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAllDisplays : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

        // Display.displays[0].Activate();
        // Display.displays[1].Activate();
        //本体の解像度設定（タイトルバーを隠さない）
        // WindowController.windowReplace("Multi Canvas Manager", 100, 100, 640, 480, false);

        //Secondary Displayの解像度設定（タイトルバー隠す）
        // WindowController.windowReplace("Unity Secondary Display", 1000, 100, 640, 640, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
