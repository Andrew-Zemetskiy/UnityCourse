// using System;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;
//
// public class TouchZoneHandler : MonoBehaviour
// {
//     private Vector2 lastTouchPosition;
//     private Vector2 touchDelta;
//     public static Action<Vector2> OnTouchDelta;
//     private InputSystem_Actions _inputSystem;
//
//     private void OnEnable()
//     {
//         _inputSystem = GetComponent<PlayerMovement>().inputSystem;
//         _inputSystem.Player.Touch.performed += OnTouchPerformed;
//         _inputSystem.Player.Touch.canceled += OnTouchCancelled;
//         _inputSystem.Enable();
//     }
//
//     private void OnDisable()
//     {
//         _inputSystem.Player.Touch.performed -= OnTouchPerformed;
//         _inputSystem.Player.Touch.canceled -= OnTouchCancelled;
//         _inputSystem.Disable();
//     }
//
//     private void OnTouchPerformed(InputAction.CallbackContext context)
//     {
//         Vector2 touchPosition = context.ReadValue<Vector2>();
//
//         if (touchPosition.x > Screen.width / 2)
//         {
//             if (lastTouchPosition != Vector2.zero)
//             {
//                 touchDelta = touchPosition - lastTouchPosition;
//             }
//
//             if (lastTouchPosition == touchPosition)
//             {
//                 OnTouchDelta?.Invoke(Vector2.zero);
//             }
//             else
//             {
//                 OnTouchDelta?.Invoke(touchDelta);
//             }
//             
//             lastTouchPosition = touchPosition;
//             // Debug.Log($"Delta {touchDelta}");
//         }
//         else
//         {
//             lastTouchPosition = Vector2.zero;
//             OnTouchDelta?.Invoke(Vector2.zero);
//         }
//     }
//
//     private void OnTouchCancelled(InputAction.CallbackContext context)
//     {
//         lastTouchPosition = Vector2.zero;
//         OnTouchDelta?.Invoke(Vector2.zero);
//         Debug.Log("Cancelled");
//     }
// }
