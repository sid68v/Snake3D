using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchPadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler

{
    public static TouchPadController Instance;


    public float smoothening = .1f;
    public GeneralDataController.DIRECTION direction;


    Vector2 origin, endPosition, normalizedDeltaPosition, deltaMovement;
    bool isTouched;
    int fingerId;

    Vector3 iniialDragPosition, finalDragPosition, dragDirection;


    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        isTouched = false;
        origin = Vector2.zero;
        direction = GeneralDataController.DIRECTION.FORWARD;


    }

    #region UNWANTED
    public void OnPointerDown(PointerEventData eventData)
    {

        if (!isTouched)
        {


            eventData.pointerId = fingerId;
            isTouched = true;

            origin = eventData.position;

        }

    }


    public void OnDrag(PointerEventData eventData)
    {
        if (isTouched && eventData.pointerId == fingerId)
        {

            endPosition = eventData.position;

            normalizedDeltaPosition = (endPosition - origin).normalized;

        }
    }



    public void OnPointerUp(PointerEventData eventData)
    {

        if (eventData.pointerId == fingerId)
        {
            isTouched = false;
            normalizedDeltaPosition = Vector2.zero;

        }

    }

    public Vector2 SetDestionationValue()
    {

        deltaMovement = Vector2.MoveTowards(deltaMovement, normalizedDeltaPosition, smoothening);
        return deltaMovement;


    }

    #endregion



    public void OnEndDrag(PointerEventData eventData)
    {

        dragDirection = (eventData.position - eventData.pressPosition).normalized;

        direction = GetDragDirection(dragDirection);
        Debug.Log("Direction : " + direction);

        switch (direction)
        {
            case GeneralDataController.DIRECTION.RIGHT: PlayerController.Instance.direction = PlayerController.Instance.transform.right; break;
            case GeneralDataController.DIRECTION.LEFT: PlayerController.Instance.direction = -PlayerController.Instance.transform.right; break;
            case GeneralDataController.DIRECTION.FORWARD: PlayerController.Instance.direction = PlayerController.Instance.transform.forward; break;
            //case GeneralDataController.DIRECTION.BACK: PlayerController.Instance.direction = -PlayerController.Instance.transform.forward; break;
        }


    }


    public GeneralDataController.DIRECTION GetDragDirection(Vector3 dragDirection)
    {

        GeneralDataController.DIRECTION currentDirection;

        float xValue = dragDirection.x;
        float yValue = dragDirection.y;

        if (Mathf.Abs(xValue) > Mathf.Abs(yValue))
        {
            // left or right movement
            currentDirection = (xValue > 0) ? GeneralDataController.DIRECTION.RIGHT : GeneralDataController.DIRECTION.LEFT;
        }
        else
        {
            // forward or backward movement
            currentDirection = (yValue > 0) ? GeneralDataController.DIRECTION.FORWARD : GeneralDataController.DIRECTION.BACK;
        }

        return currentDirection;

    }





}
