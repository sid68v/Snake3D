using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchPadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static TouchPadController Instance;


    public float smoothening = .1f;


    Vector2 origin, endPosition, normalizedDeltaPosition,deltaMovement;
    bool isTouched;
    int fingerId;

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


    }

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
        if(isTouched && eventData.pointerId == fingerId)
        {

            endPosition = eventData.position;

            normalizedDeltaPosition = (endPosition - origin).normalized;

        }
    }



    public void OnPointerUp(PointerEventData eventData)
    {

        if(eventData.pointerId == fingerId)
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


    // Update is called once per frame
    void Update()
    {

    }
}
