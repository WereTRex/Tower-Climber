using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float risingSpeed;

    [SerializeField] float waveSpeed;
    [SerializeField] SpriteRenderer spriteRenderer;

    //Start variables
    [SerializeField] float startDelay;
    [SerializeField] float delayRemaining; 
    [SerializeField] Vector3 startingPosition;

    void Start()
    {
        transform.position = startingPosition;
        delayRemaining = startDelay;
    }

    void Update()
    {
        if (delayRemaining <= 0) {
            transform.position = new Vector3(transform.position.x, transform.position.y + (1 * risingSpeed * Time.deltaTime), transform.position.z);
        } else {
            delayRemaining -= Time.deltaTime;
        }

        spriteRenderer.size = new Vector2 (spriteRenderer.size.x + (waveSpeed * Time.deltaTime), spriteRenderer.size.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Kill the player
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
