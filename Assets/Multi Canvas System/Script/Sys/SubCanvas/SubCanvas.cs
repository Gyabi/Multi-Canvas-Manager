using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCanvas : MonoBehaviour
{
    private SubCanvasInfo subCanvasInfo;
    // managerでsub一覧をlistで纏めているがその何番目に当たるか
    private int subCanvasTypeIndex;

    // 同一のsubcanvasの何枚目に当たるか
    private int subCanvasNum;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSubCanvasInfo(SubCanvasInfo subCanvasInfo, int subCanvasTypeIndex, int subCanvasNum)
    {
        this.subCanvasInfo = subCanvasInfo;
        this.subCanvasTypeIndex = subCanvasTypeIndex;
        this.subCanvasNum = subCanvasNum;
    }

    public SubCanvasInfo GetSubCanvasInfo()
    {
        return subCanvasInfo;
    }

    public int GetSubCanvasTypeIndex()
    {
        return subCanvasTypeIndex;
    }
    public int GetSubCanvasNum()
    {
        return subCanvasNum;
    }
}
