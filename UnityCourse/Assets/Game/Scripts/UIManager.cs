using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private List<GameObject> menuSections;
    
    public static UIManager Instance;

    private void Start()
    {
        Instance = this;
    }
    
    private void Awake()
    {
        mainMenu.SetActive(true);
        menuSections.ForEach(s => s.SetActive(false));
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }
}
