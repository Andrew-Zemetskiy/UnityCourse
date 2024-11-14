using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private List<GameObject> menuSections;

    private void Awake()
    {
        mainMenu.SetActive(true);
        menuSections.ForEach(s => s.SetActive(false));
    }
}
