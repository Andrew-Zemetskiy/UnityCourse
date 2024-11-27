using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraPanelUI : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;

    [Header("Buttons")] [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button faceButton;
    [SerializeField] private Button leftButton;


    [SerializeField] private float distance = 20f;

    private readonly Vector3 _upOffset = new Vector3(0, 1, 0);
    private readonly Vector3 _downOffset = new Vector3(0, -1, 0);
    private readonly Vector3 _faceOffset = new Vector3(0, 0, -1);
    private readonly Vector3 _leftOffset = new Vector3(1, 0, 0);

    private GameObject _targetGO;
    private Vector3 _cameraOffset = Vector3.zero;
    
    private void Start()
    {
        Init();
        Spawner.OnCurrentObjectChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target)
    {
        _targetGO = target;
    }
    
    private void Init()
    {
        upButton?.onClick.AddListener(() => ChangeCameraView(CamView.Up));
        downButton?.onClick.AddListener(() => ChangeCameraView(CamView.Down));
        faceButton?.onClick.AddListener(() => ChangeCameraView(CamView.Face));
        leftButton?.onClick.AddListener(() => ChangeCameraView(CamView.Left));
    }

    private void ChangeCameraView(CamView view)
    {
        if (_targetGO == null)
            return;
        
        switch (view)
        {
            case CamView.Up:
                _cameraOffset = _upOffset * distance;
                break;
            case CamView.Down:
                _cameraOffset = _downOffset * distance;
                break;
            case CamView.Face:
                _cameraOffset = _faceOffset * distance;
                break;
            case CamView.Left:
                _cameraOffset = _leftOffset * distance;
                break;
        }
        
        UpdateCameraPosition();
        // Vector3 targetPosition = _targetGO.transform.position;
        // viewCamera.transform.position = targetPosition + _cameraOffset;
        // viewCamera.transform.LookAt(targetPosition);
    }

    private void UpdateCameraPosition()
    {
        if (_targetGO == null)
            return;

        Vector3 targetPosition = _targetGO.transform.position;

        // Вычисляем смещение с учетом вращения объекта (только для видов Face и Left)
        if (_cameraOffset == _faceOffset * distance || _cameraOffset == _leftOffset * distance)
        {
            float targetRotationY = _targetGO.transform.eulerAngles.y;
            Vector3 rotatedOffset = Quaternion.Euler(0, targetRotationY, 0) * _cameraOffset;
            viewCamera.transform.position = targetPosition + rotatedOffset;
        }
        else
        {
            // Для вертикальных видов смещение остается неизменным
            viewCamera.transform.position = targetPosition + _cameraOffset;
        }

        // Камера смотрит на объект
        viewCamera.transform.LookAt(targetPosition);
    }
    
    private void OnDestroy()
    {
        Spawner.OnCurrentObjectChanged -= TargetChanged;
    }
    
    private enum CamView
    {
        Up,
        Down,
        Face,
        Left
    }
}