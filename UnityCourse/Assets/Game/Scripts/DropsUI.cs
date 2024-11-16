using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropsUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private TextMeshProUGUI textLabel;

    private Dictionary<int, string> _dropTextDic = new()
    {
        {0, "Option A"},
        {1, "Option B"},
        {2, "Option C"},
        {3, "Option D"}
    };

    private void Start()
    {
        dropdown.onValueChanged.AddListener(OnDropDownValueChanged);
        OnDropDownValueChanged(dropdown.value);
    }
    
    private void OnDropDownValueChanged(int index)
    {
        if (_dropTextDic.TryGetValue(index, out var text))
        {
            textLabel.text = text;
        }
    }
}
