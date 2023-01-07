using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static Action<Vector2> OnTouchStarted;
    public static Action<Vector2> OnTouchMoving;
    public static Action<Vector2> OnTouchReleased;

   
    //private void OnEnable()
    //{
    //    actionMap.Enable();
    //}
    //private void OnDisable()
    //{
    //    actionMap.Disable();
    //}

    //private void Start()
    //{
    //    actionMap.Player.FirstTouch.started += FirstTouch_Pressed;
    //    actionMap.Player.FirstTouch.performed += Swipe_Moving;
    //    actionMap.Player.FirstTouch.canceled += Swipe_Released;
    //}


    //private void FirstTouch_Pressed(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("start: " + obj.phase);
    //    if (OnTouchStarted != null)
    //        OnTouchStarted(actionMap.Player.Swipe.ReadValue<Vector2>());
    //}
    //private void Swipe_Moving(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("moving: " + obj.phase);
    //    if (OnTouchMoving != null)
    //        OnTouchMoving(actionMap.Player.Swipe.ReadValue<Vector2>());
    //}
    //private void Swipe_Released(InputAction.CallbackContext obj)
    //{
    //    Debug.Log("released: " + obj.phase);
    //    if (OnTouchReleased != null)
    //        OnTouchReleased(actionMap.Player.Swipe.ReadValue<Vector2>());
    //}
}
