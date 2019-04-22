using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject foodPrefab;
    public Vector2 xLimits, zLimits;
    public Button pauseButton;
    public Sprite[] pauseToggleSprites;

    [HideInInspector]
    public int score;

    [HideInInspector]
    public bool isFoodExisting;

    // Start is called before the first frame update

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("topscore"))
            PlayerPrefs.SetInt("topscore", 0);

        if (!Instance)
            Instance = this;

    }


    void Start()
    {
        //Debug.Log(PlayerPrefs.GetInt("topscore"));

        score = 0;
        isFoodExisting = false;
        pauseButton.GetComponent<Image>().sprite = pauseToggleSprites[0];

        StartCoroutine(CreateFoodAtRandom());

    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        pauseButton.GetComponent<Image>().sprite = pauseButton.GetComponent<Image>().sprite == pauseToggleSprites[0] ? pauseToggleSprites[1] : pauseToggleSprites[0];

    }


    public void ExitGame()
    {
        Application.Quit();
    }


    public IEnumerator CreateFoodAtRandom()
    {
        while (true)
        {
            yield return new WaitWhile(() => isFoodExisting);

            GameObject foodGO = Instantiate(foodPrefab);
            foodGO.transform.position = new Vector3(Random.Range(xLimits.x, xLimits.y), 5, Random.Range(zLimits.x, zLimits.y));

            isFoodExisting = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
