using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button buttonsBtn;
    [SerializeField] private Button togglesBtn;
    [SerializeField] private Button dropsBtn;
    [SerializeField] private Button inputBtn;
    [SerializeField] private Button scrollViewBtn;

    [Header("Menu Sections")]
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject toggles;
    [SerializeField] private GameObject drops;
    [SerializeField] private GameObject input;
    [SerializeField] private GameObject scrollView;

    private void Start()
    {
        buttonsBtn.onClick.AddListener(() => Show(buttons));
        togglesBtn.onClick.AddListener(() => Show(toggles));
        dropsBtn.onClick.AddListener(() => Show(drops));
        inputBtn.onClick.AddListener(() => Show(input));
        scrollViewBtn.onClick.AddListener(() => Show(scrollView));
    }

    private void OnDestroy()
    {
        buttonsBtn.onClick.RemoveAllListeners();
        togglesBtn.onClick.RemoveAllListeners();
        dropsBtn.onClick.RemoveAllListeners();
        inputBtn.onClick.RemoveAllListeners();
        scrollViewBtn.onClick.RemoveAllListeners();
    }

    private void Show(GameObject target)
    {
        this.gameObject.SetActive(false);
        target.SetActive(true);
    }
}