using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


    Rigidbody rb;

    [HideInInspector]
    public AudioSource audioSource;

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
            isFood = true;


        // this means game over.
        if (other.CompareTag("boundary") || other.CompareTag("body"))
        {

            // pause game
            Time.timeScale = 0;

            // vibrate
            Handheld.Vibrate();

            Debug.Log("Hit!");

            int currentScore = GameController.Instance.score;
            int topScore = PlayerPrefs.GetInt("topscore");

            // set current score text.
            GameController.Instance.gameOverPanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = currentScore.ToString(); // current score

            // if current score beats top score, replaces the topscore with current and displays it. else keeps and shows the old top score.
            if (currentScore >= topScore)
            {
                PlayerPrefs.SetInt("topscore", currentScore);
                GameController.Instance.gameOverPanel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = currentScore.ToString(); // top score
            }
            else
                GameController.Instance.gameOverPanel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = topScore.ToString();


            // enables the game over UI panel.
            GameController.Instance.gameOverPanel.SetActive(true);
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
        //else if (Input.GetKeyDown(KeyCode.DownArrow))
        //    direction = -transform.forward;

    }

}
