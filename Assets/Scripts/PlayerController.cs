using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;
    //public TouchPadController touchPad;

    public float jumpHeight = .2f;
    public float speed = 2f;
    public float tilt = 2f;
    public float jumpFrequency = 1f;
    public float distance = 2.5f;
    public bool isFood;
    public bool isTouchpadControlled;
    public GameObject bodyPrefab;
    public Vector3 direction;

    public AudioClip eatClip;
    public AudioClip dieClip;
    public AudioClip hissClip;

    AudioSource audioSource;



    Rigidbody rb;
    [HideInInspector]
    List<Transform> tail = new List<Transform>();



    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        //isTouchpadControlled = (Application.platform == RuntimePlatform.Android) ? true : false;

        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        isFood = false;
        direction = Vector3.forward;

        GameObject[] go = GameObject.FindGameObjectsWithTag("body");
        foreach (GameObject body in go)
            tail.Add(body.transform);

        StartCoroutine(MovePlayer());

    }

    // on hitting oher colliders, if food, grow, else it means hitting either the wall or the body, implying death and restart.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("food"))
        {
            isFood = true;
            //GameController.Instance.DestroyFood();
            GameController.Instance.isFoodExisting = false;
        }

        if (other.CompareTag("boundary") || other.CompareTag("body"))
        {
            Debug.Log("Hit!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public IEnumerator MovePlayer()
    {

        while (true)
        {
            yield return new WaitForEndOfFrame();
            Move();

        }
    }

    public void Move()
    {

        Vector3 headPosition = transform.position;

        transform.LookAt(direction + transform.position);

        transform.Translate(new Vector3(direction.x, Mathf.Cos(Time.timeSinceLevelLoad * jumpFrequency) * jumpHeight, direction.z) * speed * Time.deltaTime, Space.World);

        if (isFood)
        {
            Debug.Log("ATE FOOD");
            GameObject addTail = Instantiate(bodyPrefab, headPosition, Quaternion.identity);
            addTail.tag = "body";
            tail.Insert(0, addTail.transform);
            isFood = false;
        }

        if (tail.Count > 0 && Vector3.Distance(tail.First().position, headPosition) > distance)
        {
            tail.Last().position = headPosition;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = -transform.right;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = transform.right;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = transform.forward;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = -transform.forward;

        Debug.Log(TouchPadController.Instance.direction);


    }


}
