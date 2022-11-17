using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerController : MonoBehaviour
{
    private Vector2 joystickSize = new Vector2(300, 300);
    [SerializeField]
    private FloatingJoystick joystick;

    private Rigidbody pRigidBody;
    private Animator pAnimator;

    public float speed = 20f;
    public StatsSO playerStats;


    private Finger movementFinger;
    private Vector2 movementAmount;
    private void OnEnable()
    {
        //InputController.OnTouchStarted += Touch_Pressed;
        //InputController.OnTouchMoving += Touch_Moving;
        //InputController.OnTouchReleased += Touch_Released;

        EnhancedTouchSupport.Enable();

        ETouch.Touch.onFingerDown += Touch_onFingerDown;
        ETouch.Touch.onFingerMove += Touch_onFingerMove;
        ETouch.Touch.onFingerUp += Touch_onFingerUp;


    }

    private void Start()
    {
        pRigidBody = GetComponent<Rigidbody>();
        pAnimator = GetComponentInChildren<Animator>();
    }

    //private void Touch_Pressed(Vector2 fingerPositon)
    //{
    //    print("finger pos" + fingerPositon);
    //    movementAmount = Vector2.zero;
    //    joystick.gameObject.SetActive(true);
    //    joystick.RectTransform.sizeDelta = joystickSize;
    //    joystick.RectTransform.anchoredPosition = ClampStartPosition(fingerPositon);
    //    pAnimator.SetTrigger("Run");
    //}
    private void Touch_onFingerDown(Finger TouchedFinger)
    {
        if (movementFinger == null)
        {
            movementFinger = TouchedFinger;
            movementAmount = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.RectTransform.sizeDelta = joystickSize;
            joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
            pAnimator.SetTrigger("Run");
        }
    }

    //private void Touch_Released(Vector2 fingerPositon)
    //{
    //    print("finger pos" + fingerPositon);
    //    joystick.Knob.anchoredPosition = Vector2.zero;
    //    joystick.gameObject.SetActive(false);
    //    movementAmount = Vector2.zero;
    //    pAnimator.SetTrigger("Idle");
    //}

    private void Touch_onFingerUp(Finger ReleasedFinger)
    {
        if (ReleasedFinger == movementFinger)
        {
            movementFinger = null;
            joystick.Knob.anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            movementAmount = Vector2.zero;
            pAnimator.SetTrigger("Idle");
        }
    }

    //private void Touch_Moving(Vector2 fingerPositon)
    //{
    //    print("finger pos" + fingerPositon);
    //    Vector2 knobPosition;
    //    float maxMovement = joystickSize.x / 2f;

    //    if (Vector2.Distance(fingerPositon,
    //        joystick.RectTransform.anchoredPosition) > maxMovement)
    //    {
    //        knobPosition = (
    //            fingerPositon - joystick.RectTransform.anchoredPosition
    //            ).normalized
    //            * maxMovement;
    //    }
    //    else
    //    {
    //        knobPosition = fingerPositon - joystick.RectTransform.anchoredPosition;
    //    }

    //    joystick.Knob.anchoredPosition = knobPosition;
    //    movementAmount = knobPosition / maxMovement;
    //    pAnimator.SetFloat("speedMultipler", movementAmount.magnitude);
    //    //print("movementAmount.magnitude" + movementAmount.magnitude);
    //}
    private void Touch_onFingerMove(Finger MovedFinger)
    {
        Vector2 knobPosition;
        float maxMovement = joystickSize.x / 2f;
        ETouch.Touch currentTouch = MovedFinger.currentTouch;

        if (Vector2.Distance(currentTouch.screenPosition,
            joystick.RectTransform.anchoredPosition) > maxMovement)
        {
            knobPosition = (
                currentTouch.screenPosition - joystick.RectTransform.anchoredPosition
                ).normalized
                * maxMovement;
        }
        else
        {
            knobPosition = currentTouch.screenPosition - joystick.RectTransform.anchoredPosition;
        }

        joystick.Knob.anchoredPosition = knobPosition;
        movementAmount = knobPosition / maxMovement;
        pAnimator.SetFloat("speedMultipler", movementAmount.magnitude);
    }


    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < joystickSize.x / 2)
        {
            startPosition.x = joystickSize.x / 2;
        }
        if (startPosition.x > Screen.width - joystickSize.x / 2)
        {
            startPosition.x = Screen.width - joystickSize.x / 2;
        }
        if (startPosition.y < joystickSize.y / 2)
        {
            startPosition.y = joystickSize.y / 2;
        }
        if(startPosition.y > Screen.height - joystickSize.y / 2)
        {
            startPosition.y = Screen.height - joystickSize.y / 2;
        }
        return startPosition;
    }
    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= Touch_onFingerDown;
        ETouch.Touch.onFingerMove -= Touch_onFingerMove;
        ETouch.Touch.onFingerUp -= Touch_onFingerUp;

        //InputController.OnTouchStarted -= Touch_Pressed;
        //InputController.OnTouchMoving -= Touch_Moving;
        //InputController.OnTouchReleased -= Touch_Released;
    }

    private void Update()
    {
        Vector3 scaledMovement = speed * Time.deltaTime * new Vector3(movementAmount.x, 0, movementAmount.y);
        transform.LookAt(transform.position + scaledMovement, Vector3.up);
        pRigidBody.MovePosition(transform.position + scaledMovement);
    }

    
}
