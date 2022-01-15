using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritModeManager : MonoBehaviour
{
    // 手動でインスペクターから設定したカメラ情報
    private List<CamInfo> CamList = new List<CamInfo>();

    // 現在の分割モード
    private SplitMode splitMode;

    // 各モードで画面を何分割するか記載した定数
    private static Dictionary<SplitMode, int> splitModeDic = new Dictionary<SplitMode, int>()
    {
        { SplitMode.One, 1 },
        { SplitMode.Two, 2 },
        { SplitMode.Three_one, 3 },
        { SplitMode.Three_two, 3 },
        { SplitMode.Four, 4 }
    };

    // 設定したカメラの数
    private int CamNum;

    // 実際に描画するカメラのインデックス
    // 3分割なら[1,0,3]のような形で格納される
    private List<int> SelectedCamIndexList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // managerからstartで呼ばれる関数
    public void SetData(List<CamInfo> camList)
    {
        CamList = camList;
        CamNum = CamList.Count;
    }

    // セットした値に応じて画面分割を変更する
    public void ChangeSplitMode(SplitMode mode)
    {
        splitMode = mode;
        // 使用するカメラオブジェクト特定
        // すでに登録されているカメラが存在しないならindex順に登録する
        if(SelectedCamIndexList.Count == 0)
        {
            for(int i = 0; i < splitModeDic[mode]; i++)
            {
                SelectedCamIndexList.Add(i);
            }
        }else if(SelectedCamIndexList.Count < splitModeDic[mode])
        {
            // 登録されているカメラが足りない場合はindex順で追加する
            int i = 0;
            int j = 0;
            int loop = splitModeDic[mode] - SelectedCamIndexList.Count;
            while(i < loop)
            {
                if(!SelectedCamIndexList.Contains(j))
                {
                    // Debug.Log("not contains");

                    SelectedCamIndexList.Add(j);
                    i++;
                }
                // Debug.Log(j);
                j++;
            }
        }else if(SelectedCamIndexList.Count > splitModeDic[mode])
        {
            // 登録されているカメラが多い場合はPOPで削除する
            int loop = SelectedCamIndexList.Count - splitModeDic[mode];
            for(int i = 0; i < loop; i++)
            {
                SelectedCamIndexList.RemoveAt(SelectedCamIndexList.Count - 1);
            }
        }

        // 更新した内容で分割を実行
        ExecuteSplit(mode);
    }

    private void ExecuteSplit(SplitMode mode)
    {
        // 特定したオブジェクト表示、非表示
        for(int i = 0; i < CamNum; i++)
        {
            if(SelectedCamIndexList.Contains(i))
            {
                CamList[i].Cam.gameObject.SetActive(true);
            }else
            {
                CamList[i].Cam.gameObject.SetActive(false);
            }
        }

        // modeに応じて分割比率を変更
        switch(mode)
        {
            case SplitMode.One:
                CamList[SelectedCamIndexList[0]].Cam.rect = new Rect(0, 0, 1, 1);
                break;
            case SplitMode.Two:
                CamList[SelectedCamIndexList[0]].Cam.rect = new Rect(0, 0, 0.5f, 1);
                CamList[SelectedCamIndexList[1]].Cam.rect = new Rect(0.5f, 0, 0.5f, 1);
                break;
            case SplitMode.Three_one:
                CamList[SelectedCamIndexList[0]].Cam.rect = new Rect(0, 0, 0.5f, 1);
                CamList[SelectedCamIndexList[1]].Cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                CamList[SelectedCamIndexList[2]].Cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
            case SplitMode.Three_two:
                CamList[SelectedCamIndexList[0]].Cam.rect = new Rect(0.5f, 0, 0.5f, 1);
                CamList[SelectedCamIndexList[1]].Cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                CamList[SelectedCamIndexList[2]].Cam.rect = new Rect(0, 0, 0.5f, 0.5f);
                break;
            case SplitMode.Four:
                CamList[SelectedCamIndexList[0]].Cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                CamList[SelectedCamIndexList[1]].Cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                CamList[SelectedCamIndexList[2]].Cam.rect = new Rect(0, 0, 0.5f, 0.5f);
                CamList[SelectedCamIndexList[3]].Cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
                
        }
    }

    // 引数でCamlistに対応するindexのリストを受け取り、内容を反映、その後実際にカメラ分割を再実行する
    public void ChangeSelectedCamIndexList(List<int> indexList)
    {
        if(indexList.Count == splitModeDic[splitMode])
        {
            SelectedCamIndexList = indexList;
            ExecuteSplit(splitMode);
        }
    }

    public bool SplitModeChangeJudge(SplitMode mode)
    {
        return CamNum >= splitModeDic[mode];
    }

    public int GetSplitModeDicNum(SplitMode mode)
    {
        return splitModeDic[mode];
    }

    public List<int> GetSelectedCamIndexList()
    {
        return SelectedCamIndexList;
    }
}
