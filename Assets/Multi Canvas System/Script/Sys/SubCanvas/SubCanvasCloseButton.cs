using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCanvasCloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickSubCanvasCloseButton()
    {
        CanvasManager.Instance.DeleteSubCanvas(GetComponent<Canvas>(), GetComponent<SubCanvas>().GetSubCanvasInfo(), GetComponent<SubCanvas>().GetSubCanvasTypeIndex());
    }
}
