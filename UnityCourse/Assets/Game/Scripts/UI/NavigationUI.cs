using System;
using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    [SerializeField] private Button previousBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button[] colorPanelBtns;
    
    public static Action<bool> OnNavigate;
    public static Action<ColorLib> OnColorChanged;
    
    private void Start()
    {
        InitButtons();
    }

    private void InitButtons()
    {
        previousBtn.onClick.AddListener(() => TriggerNavigation(false));
        nextBtn.onClick.AddListener(() => TriggerNavigation(true));
        
        colorPanelBtns[0]?.onClick.AddListener(() => TriggerColorChange(ColorLib.Red));
        colorPanelBtns[1]?.onClick.AddListener(() => TriggerColorChange(ColorLib.Blue));
        colorPanelBtns[2]?.onClick.AddListener(() => TriggerColorChange(ColorLib.Yellow));
        colorPanelBtns[3]?.onClick.AddListener(() => TriggerColorChange(ColorLib.Green));
        
    }
    
    private void TriggerNavigation(bool isForward)
    {
        OnNavigate?.Invoke(isForward);
    }

    private void TriggerColorChange(ColorLib color)
    {
        OnColorChanged?.Invoke(color);
    }
    
    private void OnDestroy()
    {
        RemoveListeners(previousBtn);
        RemoveListeners(nextBtn);
        RemoveListeners(colorPanelBtns);
    }

    private void RemoveListeners(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button?.onClick.RemoveAllListeners();
        }
    }
}