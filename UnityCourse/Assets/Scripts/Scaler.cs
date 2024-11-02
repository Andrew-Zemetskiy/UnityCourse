using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float scaleSpeed = 1f;

    [SerializeField] private Vector3 targetScale;
    private Vector3 _originalScale;


    private void Start()
    {
        _originalScale = targetObject.localScale;
    }

    private void Update()
    {
        targetObject.localScale = Vector3.Lerp(_originalScale, targetScale, Mathf.PingPong(Time.time * scaleSpeed, 1f));
    }
}