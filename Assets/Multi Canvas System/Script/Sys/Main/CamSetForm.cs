using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class CamSetForm : MonoBehaviour
{
    // 分割設定で各分割画面に移すカメラを設定するためのスクリプト
    SplitMode mode;
    private List<CamInfo> CamList = new List<CamInfo>();
    private List<int> SelectedCamIndexList = new List<int>();

    [SerializeField, Header("分割画面指定用のDropdown")]
    private List<Dropdown> dropdownList = new List<Dropdown>();

    [SerializeField, Header("Warnning表示要のPanel")]
    private GameObject WarningPanel;


    // Start is called before the first frame update
    void Start()
    {
        mode = CanvasManager.Instance.GetSplitMode();
        CamList = CanvasManager.Instance.GetCamList();
        SelectedCamIndexList = CanvasManager.Instance.GetSelectedCamIndexList();
        CreateCamSetForm();
    }

    // Update is called once per frame
    void Update()
    {
        // splitmodeの変化を監視
        if(CanvasManager.Instance.GetSplitMode() != mode)
        {
            mode = CanvasManager.Instance.GetSplitMode();
            SelectedCamIndexList = CanvasManager.Instance.GetSelectedCamIndexList();
            CreateCamSetForm();
        }
    }
    // ドロップダウンの表示可否、設定値をmanagerから得た情報をもとに更新する
    void CreateCamSetForm()
    {
        // リストにinfoの名称を格納して反映
        List<string> options = new List<string>();
        for(int i = 0; i < CamList.Count; i++)
        {
            options.Add(CamList[i].Name);
        }
        for(int i = 0; i < dropdownList.Count; i++)
        {
            dropdownList[i].ClearOptions();
            dropdownList[i].AddOptions(options);
            if(SelectedCamIndexList.Count > i)
            {
                dropdownList[i].value = SelectedCamIndexList[i];
            }else{
                dropdownList[i].value = 0;
            }
            if(CanvasManager.Instance.GetSplitModeDicNum(mode) > i)
            {
                dropdownList[i].transform.parent.gameObject.SetActive(true);
            }else{
                dropdownList[i].transform.parent.gameObject.SetActive(false);
            }
        }
        
    }

    // executeボタンが押下されたときの処理
    public void OnClickFormExecute()
    {
        int loop = CanvasManager.Instance.GetSplitModeDicNum(mode);

        List<int> tmpList = new List<int>();
        // ドロップダウンから必要な情報を吸い上げてCanvasManagerに渡す
        for(int i = 0; i < loop; i++)
        {
            tmpList.Add(dropdownList[i].value);
        }
        // 重複して洗濯していた場合はwarningを表示
        var ListEleTypeNum = tmpList.GroupBy(x => x).Count();
        if(ListEleTypeNum != loop)
        {
            WarningPanel.SetActive(true);
            return;
        }

        SelectedCamIndexList = tmpList;
        CanvasManager.Instance.ChangeSelectedCamIndexList(SelectedCamIndexList);
    }
}
