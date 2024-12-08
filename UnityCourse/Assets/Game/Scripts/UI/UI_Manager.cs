using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoTypeText;

    private void Start()
    {
        AmmoTypeChanger.OnTriggerEntered += ProjectileTypeChange;
    }

    private void ProjectileTypeChange(ProjectilesType obj)
    {
        ammoTypeText.text = new string($"Projectile Type: {obj}");
    }
}
