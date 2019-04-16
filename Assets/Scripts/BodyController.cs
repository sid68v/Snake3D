using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{

    public Transform target;
    public float maxDistanceDelta = 2f;


    public bool isFollowingHead;
    public int bodyScale = 5;// Vector3.one * 5;
    public float smoothening = 5f;

    GameObject[] bodyObjects;
    GameObject lastBodyGO,headGO;
    Rigidbody rb,headRigidBody;



    // Start is called before the first frame update
    void Start()
    {

        bodyObjects = GameObject.FindGameObjectsWithTag("body");
        headGO = GameObject.FindGameObjectWithTag("head");

        rb = GetComponent<Rigidbody>();
        headRigidBody = headGO.GetComponent<Rigidbody>();

        lastBodyGO = bodyObjects[bodyObjects.Length-1];

    }

    private void LateUpdate()
    {
        //if (Vector3.Distance(transform.position, target.position) > maxDistanceDelta)
        {
            transform.position = target.position - target.forward * maxDistanceDelta;
        }
    }


    //private void FixedUpdate()
    //{
    //    if (!isFollowingHead)
    //    {


    //        rb.MovePosition(Vector3.MoveTowards( rb.position,lastBodyGO.transform.position + rb.velocity.normalized * bodyScale,smoothening));
            
    //    }
    //    else
    //    {
    //        rb.MovePosition(Vector3.MoveTowards(rb.position, headRigidBody.transform.position + rb.velocity.normalized * bodyScale, smoothening));

    //    }


    //}

    // Update is called once per frame
    void Update()
    {

    }
}
