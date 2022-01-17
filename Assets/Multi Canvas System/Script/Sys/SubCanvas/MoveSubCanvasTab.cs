using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveSubCanvasTab : MonoBehaviour
{
    // アタッチの際に子オブジェクトにもhitboxが適応されてしまうので子オブジェクトのRaycastTargetをfalseにする
    private RectTransform rectTransform;
    private RectTransform pearentRectTransform;

   // Start is called before the first frame update
    void Start()
    {
        // 自身と親のRectTransformを取得
        rectTransform = GetComponent<RectTransform>();
        pearentRectTransform = transform.parent.GetComponent<RectTransform>();

        // イベントトリガーの設定
        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrag(PointerEventData data)
    {
        // // Debug.Log(data.currentInputModule.ToString());
        // Vector3 mousePos = Input.mousePosition;
        // // 描画範囲にタブが収まるときにはドラッグで移動させる
        // if(rectTransform.sizeDelta.x/2 > Input.mousePosition.x) 
        // {
        //     mousePos.x = rectTransform.sizeDelta.x/2;
        // }
        // if(Input.mousePosition.x > pearentRectTransform.sizeDelta.x - rectTransform.sizeDelta.x/2)
        // {
        //     mousePos.x = pearentRectTransform.sizeDelta.x - rectTransform.sizeDelta.x/2;
        // }
        // if(rectTransform.sizeDelta.y/2 > Input.mousePosition.y)
        // {
        //     mousePos.y = rectTransform.sizeDelta.y/2;
        // }
        // if(Input.mousePosition.y > pearentRectTransform.sizeDelta.y - rectTransform.sizeDelta.y/2)
        // {
        //     mousePos.y = pearentRectTransform.sizeDelta.y - rectTransform.sizeDelta.y/2;
        // }
        // rectTransform.position = mousePos;

        // // Debug.Log(data.currentInputModule.ToString());
        Vector3 mousePos = Input.mousePosition;
        // // 描画範囲にタブが収まるときにはドラッグで移動させる
        if(rectTransform.sizeDelta.x/2 > Input.mousePosition.x) 
        {
            mousePos.x = rectTransform.sizeDelta.x/2;
        }
        if(Input.mousePosition.x > Screen.width - rectTransform.sizeDelta.x/2)
        {
            mousePos.x = Screen.width - rectTransform.sizeDelta.x/2;
        }
        if(rectTransform.sizeDelta.y/2 > Input.mousePosition.y)
        {
            mousePos.y = rectTransform.sizeDelta.y/2;
        }
        if(Input.mousePosition.y > Screen.height - rectTransform.sizeDelta.y/2)
        {
            mousePos.y = Screen.height - rectTransform.sizeDelta.y/2;
        }
        rectTransform.position = mousePos;
    }
}
