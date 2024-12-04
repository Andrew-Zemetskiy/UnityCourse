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

    private readonly Vector3 _upOffset = new(0, 1, 0);
    private readonly Vector3 _downOffset = new(0, -1, 0);
    private readonly Vector3 _faceOffset = new(0, 0, 1);
    private readonly Vector3 _leftOffset = new(-1, 0, 0);

    private GameObject _targetGO;

    private CamView _currentCamView;
    private Vector3 _cameraOffset = Vector3.zero;
    private Vector3 _cameraRotation;

    private void Start()
    {
        Init();
        Spawner.OnCurrentObjectChanged += TargetChanged;
        Rotator.OnRotated += UpdatePosition;
    }

    private void UpdatePosition()
    {
        if (_targetGO != null)
        {
            UpdateCameraPosition();
        }
    }

    private void TargetChanged(GameObject target)
    {
        _targetGO = target;
        upButton?.onClick.Invoke();
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

        _currentCamView = view;
        switch (_currentCamView)
        {
            case CamView.Up:
                _cameraOffset = _upOffset * distance;
                _cameraRotation = new Vector3(90, 180, 0);
                break;
            case CamView.Down:
                _cameraOffset = _downOffset * distance;
                _cameraRotation = new Vector3(-90, 0, 0);
                break;
            case CamView.Face:
                _cameraOffset = _faceOffset * distance;
                _cameraRotation = new Vector3(0, 180, 0);
                break;
            case CamView.Left:
                _cameraOffset = _leftOffset * distance;
                _cameraRotation = new Vector3(0, 90, 0);
                break;
        }

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if (_targetGO == null)
            return;

        Vector3 targetPosition = _targetGO.transform.position;
        float targetRotationY = _targetGO.transform.eulerAngles.y;

        if (_currentCamView == CamView.Face | _currentCamView == CamView.Left)
        {
            Vector3 rotatedOffset = Quaternion.Euler(0, targetRotationY, 0) * _cameraOffset;
            viewCamera.transform.position = targetPosition + rotatedOffset;
            viewCamera.transform.rotation = _targetGO.transform.rotation * Quaternion.Euler(_cameraRotation);
        }
        else
        {
            viewCamera.transform.position = targetPosition + _cameraOffset;
            viewCamera.transform.rotation = _targetGO.transform.rotation * Quaternion.Euler(_cameraRotation);
        }
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