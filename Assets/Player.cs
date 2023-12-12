using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    int coins;

    [SerializeField] TextMeshProUGUI tmpro;

    [SerializeField] AudioClip coinCollect;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpro.text = "Coins: " + coins;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinCollect);

            coins += 1;
            Destroy(collision.gameObject); 
        }
    }

}
