using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    public float distance = 10f;
    public float height = 10f;

    public float rotationDamping = 2f;
    public float heightDamping = 2f;

  //  WaitForEndOfFrame WAIT_FOR_FRAME;

    // Start is called before the first frame update
    void Start()
    {
     //   WAIT_FOR_FRAME = new WaitForEndOfFrame();
        StartCoroutine(Follow());
    }

    // Follow the player in movement and rotation.
    public IEnumerator Follow()
    {

        while (true)
        {
            //manage initial and wanted camera rotation and position
            float wantedRotationAngle = player.transform.localEulerAngles.y;
            float currentRotationAngle = transform.localEulerAngles.y;
            float wantedHeight = player.transform.position.y + height;
            float currentHeight = transform.position.y;

            // smoothe shift in angle
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            //smooth shift in position
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // convert angle to rotation
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            //shift the camera position
            Vector3 position = player.transform.position; //transform.position;    //change to next two commented code to move the camera in a cinematic fashion.

            position = position - currentRotation * Vector3.forward * distance;  //position - currentRotation * -transform.forward * distance;
            position.y = currentHeight;

            //set position
            transform.position = position;

            //always look at the head
            transform.LookAt(player.transform);

            //yield return WAIT_FOR_FRAME;

            yield return new WaitForEndOfFrame();
        }
    }

}
