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

    private Vector3 _upOffset = new Vector3(0, 1, 0);
    private Vector3 _downOffset = new Vector3(0, -1, 0);
    private Vector3 _faceOffset = new Vector3(0, 0, -1);
    private Vector3 _leftOffset = new Vector3(1, 0, 0);

    private GameObject targetGO;
    private void Start()
    {
        Init();
        Spawner.OnCurrentObjectChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target)
    {
        targetGO = target;
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
        if (targetGO == null)
            return;
        
        Vector3 targetPosition = targetGO.transform.position;
        Vector3 newPosition = Vector3.zero;
        
        switch (view)
        {
            case CamView.Up:
                newPosition = targetPosition + _upOffset * distance;
                Debug.Log($"Change camera view to Up: {_upOffset}");
                break;
            case CamView.Down:
                newPosition = targetPosition + _downOffset * distance;
                Debug.Log($"Change camera view to Down: {_downOffset}");
                break;
            case CamView.Face:
                newPosition = targetPosition + _faceOffset * distance;
                Debug.Log($"Change camera view to Face: {_faceOffset}");
                break;
            case CamView.Left:
                newPosition = targetPosition + _leftOffset * distance;
                Debug.Log($"Change camera view to Left: {_leftOffset}");
                break;
        }
        
        viewCamera.transform.position = newPosition;
        viewCamera.transform.LookAt(targetPosition);
        // viewCamera.transform.rotation = Quaternion.LookRotation(targetPosition - viewCamera.transform.position);
        
        Debug.Log($"Camera moved to {newPosition} and looking at {targetPosition}");
    }

    private enum CamView
    {
        Up,
        Down,
        Face,
        Left
    }
}