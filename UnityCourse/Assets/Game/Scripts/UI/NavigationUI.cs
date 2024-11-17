using System;
using UnityEngine;
using UnityEngine.UI;

public class NavigationUI : MonoBehaviour
{
    [SerializeField] private Button previousBtn;
    [SerializeField] private Button nextBtn;

    public static Action<bool> OnNavigate;

    private void Start()
    {
        previousBtn.onClick.AddListener(() => TriggerNavigation(false));
        nextBtn.onClick.AddListener(() => TriggerNavigation(true));
    }


    private void TriggerNavigation(bool isForward)
    {
        OnNavigate?.Invoke(isForward);
    }
}