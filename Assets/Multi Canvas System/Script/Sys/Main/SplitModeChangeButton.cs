using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplitModeChangeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // イベントトリガーの設定
        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnClickSplitModeChangeButton(); });
        trigger.triggers.Add(entry);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickSplitModeChangeButton()
    {
        // スプリットモードを切り替える
        SplitMode mode;
        string name = gameObject.name;
        switch(name)
        {
            case "One":
                mode = SplitMode.One;
                break;
            case "Two":
                mode = SplitMode.Two;
                break;
            case "Three_one":
                mode = SplitMode.Three_one;
                break;
            case "Three_two":
                mode = SplitMode.Three_two;
                break;
            case "Four":
                mode = SplitMode.Four;
                break;
            default:
                mode = SplitMode.One;
                break;
        }


        CanvasManager.Instance.SetCamMode(mode);
        Debug.Log("SplitModeChangeButton");
    }
}
