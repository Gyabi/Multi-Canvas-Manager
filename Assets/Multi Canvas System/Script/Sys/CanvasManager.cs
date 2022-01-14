using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance;

    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private List<SubCanvasInfo> SubCanvasList = new List<SubCanvasInfo>();

    [SerializeField]
    private List<CamInfo> CamList = new List<CamInfo>();

    // サブキャンバス数
    private int SubCanvasNum;
    // メインキャンバスのインスタンス
    private Canvas MainCanvasInstance;
    // サブキャンバスのインスタンスを格納する配列
    private List<Canvas> SubCanvasInstanceList = new List<Canvas>();
    // SubCanvasListの中の各canvasがいくつインスタンス化されたか格納する配列
    private List<int> SelectedSubCanvasIndexList = new List<int>();



    // ここから画面分割関連
    private SplitMode splitMode;

    private GameObject splitModeManager;


    // シングルトン
    public static CanvasManager Instance
    {
        get
        {
            if(_instance == null)
            {
                // 既に存在するインスタンスを探す
                _instance = (CanvasManager)FindObjectOfType(typeof(CanvasManager));
            }
            return _instance;
        }
        set
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        SubCanvasNum = SubCanvasList.Count;
        // CamNum = CamList.Count;

        for(int i = 0; i < SubCanvasNum; i++)
        {
            SelectedSubCanvasIndexList.Add(0);
        }

        // mainキャンバスインスタンス化
        MainCanvasInstance = Instantiate(MainCanvas);
        MainCanvasInstance.gameObject.transform.Find("SettingPanel").gameObject.SetActive(false);

        // 画面分割初期化
        splitModeManager = new GameObject("SplitModeManager");
        splitModeManager.AddComponent<SpritModeManager>();
        splitModeManager.GetComponent<SpritModeManager>().SetData(CamList);
        splitMode = SplitMode.One;
        SetCamMode(splitMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSettingShow()
    {
        bool ActiveSetting = MainCanvasInstance.gameObject.transform.Find("SettingPanel").gameObject.activeInHierarchy;
        MainCanvasInstance.gameObject.transform.Find("SettingPanel").gameObject.SetActive(!ActiveSetting);
    }

    public int GetSubCanvasNum()
    {
        return SubCanvasNum;
    }

    public string GetSubCanvasName(int index)
    {
        return SubCanvasList[index].Name;
    }

    public void CreateSubCanvas(int num)
    {
        // サブキャンバスのインスタンス化
        Canvas SubCanvasInstance = Instantiate(SubCanvasList[num].Canvas);
        SubCanvasInstance.gameObject.transform.SetParent(MainCanvasInstance.transform);
        SubCanvasInstance.gameObject.transform.localPosition = Vector3.zero;
        SubCanvasInstance.gameObject.transform.localScale = Vector3.one;
        SubCanvasInstance.gameObject.transform.localRotation = Quaternion.identity;
        SubCanvasInstance.gameObject.name = SubCanvasList[num].Name+SelectedSubCanvasIndexList[num];
        // インスタンス化してきたデータを格納しているlistに追加
        SubCanvasInstanceList.Add(SubCanvasInstance);
        // インスタンス化した数を格納しているlistに反映
        SelectedSubCanvasIndexList[num] += 1;
        // 生成したインスタンス側にクラスを与えて自分のsubcanvasinfoを渡す
        SubCanvasInstance.gameObject.AddComponent<SubCanvas>();
        SubCanvasInstance.gameObject.GetComponent<SubCanvas>().SetSubCanvasInfo(SubCanvasList[num], num, SelectedSubCanvasIndexList[num]);
    }
    
    public void DeleteSubCanvas(Canvas canvas, SubCanvasInfo subCanvasInfo, int num)
    {
        // インスタンス化した数を格納しているlistに反映
        SelectedSubCanvasIndexList[num] -= 1;
        // インスタンス化したデータを格納しているlistから削除
        SubCanvasInstanceList.Remove(canvas);

        Destroy(canvas.gameObject);
    }



    // カメラモードをセット
    public void SetCamMode(SplitMode mode)
    {
        // managerにmodeを変えられるか聞く
        if(splitModeManager.GetComponent<SpritModeManager>().SplitModeChangeJudge(mode))
        {
            splitMode = mode;
            splitModeManager.GetComponent<SpritModeManager>().ChangeSplitMode(mode);
        }
    }
}
