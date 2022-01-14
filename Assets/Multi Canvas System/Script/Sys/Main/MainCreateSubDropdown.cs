using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCreateSubDropdown : MonoBehaviour
{
    Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown);
        });
        ResetDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetDropdown()
    {
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        options.Add("Generate SubCanvas");
        for (int i = 0; i < CanvasManager.Instance.GetSubCanvasNum(); i++)
        {
            options.Add(CanvasManager.Instance.GetSubCanvasName(i));
        }
        dropdown.AddOptions(options);
        dropdown.value = 0;
    }

    public void DropdownValueChanged(Dropdown change)
    {
        if (change.value == 0)
        {
            return;
        }
        CanvasManager.Instance.CreateSubCanvas(change.value - 1);
        ResetDropdown();
    }
}
