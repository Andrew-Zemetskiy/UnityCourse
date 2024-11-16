using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private TextMeshProUGUI textLabel;

    private Dictionary<string, string> _toggleTextDic = new()
    {
        {"Toggle1", "First"},
        {"Toggle2", "Second"},
        {"Toggle3", "Third"}
    };

    private void Start()
    {
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener(t => OnToggleChanged(toggle));
        }
    }
    
    private void OnToggleChanged(Toggle toggle)
    {
        if (toggle.isOn && _toggleTextDic.TryGetValue(toggle.name, out var value))
        {
            textLabel.text = value;
        }
    }

    private void OnDestroy()
    {
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}