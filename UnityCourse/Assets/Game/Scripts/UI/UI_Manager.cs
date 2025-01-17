using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoTypeText;
    [SerializeField] private GameObject controls;

    private void Start()
    {
        StartCoroutine(DisableAfterDelay(controls, 2f));
    }

    private IEnumerator DisableAfterDelay(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
    }

    private void ProjectileTypeChange(ProjectilesType obj)
    {
        ammoTypeText.text = new string($"Projectile Type: {obj}");
    }
}