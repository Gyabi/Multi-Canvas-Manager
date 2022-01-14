using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetText(transform.parent.gameObject.transform.parent.GetComponent<SubCanvas>().GetSubCanvasNum().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        GetComponent<Text>().text = text;
    }
}
