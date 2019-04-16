using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject foodPrefab;
    public Vector2 xLimits, zLimits;

    [HideInInspector]
    public bool isFoodExisting;

    // Start is called before the first frame update

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }


    void Start()
    {
        isFoodExisting = false;
        StartCoroutine(CreateFoodAtRandom());

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

    public void DestroyFood()
    {
        GameObject food = GameObject.FindGameObjectWithTag("food");
        Destroy(food);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
