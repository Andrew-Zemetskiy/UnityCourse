using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonsUI : MonoBehaviour, IMenuSectors
{
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private List<Button> buttons;

    private void Start()
    {
        buttons[0].onClick.AddListener(() => PrintText("One"));
        buttons[1].onClick.AddListener(() => PrintText("Two"));
        buttons[2].onClick.AddListener(DisableButtons);
    }

    private void PrintText(string text)
    {
        labelText.text = text;
    }

    private void OnDestroy()
    {
        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();
        buttons[2].onClick.RemoveAllListeners();
    }
    
    private void DisableButtons()
    {
        buttons.ForEach(b => b.interactable = false);
    }

    public void Show()
    {
        buttons.ForEach(b => b.interactable = true);
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}