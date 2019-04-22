using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{

    public int value = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("head"))
        {
            StartCoroutine(DestroyFood());
        }
    }

    public IEnumerator DestroyFood()
    {

        PlayerController.Instance.audioSource.clip = PlayerController.Instance.eatClip;
        PlayerController.Instance.audioSource.Play();

        yield return new WaitForSeconds(PlayerController.Instance.eatClip.length);
        
        PlayerController.Instance.audioSource.clip = PlayerController.Instance.hissClip;
        PlayerController.Instance.audioSource.Play();

        GameController.Instance.isFoodExisting = false;
        GameController.Instance.score += value;
        GameController.Instance.scoreText.text = "Score : " + GameController.Instance.score; 
        Destroy(transform.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
