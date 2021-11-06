using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] int worth;
    [SerializeField] ParticleSystem ps;
    bool grabbed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !grabbed)
        {
            UpdateForGrabbed();
            
            ScoreManager.AddScore(worth);
            Debug.Log("Added " + worth + " to score");
        }
    }

    void UpdateForGrabbed()
    {
        grabbed = true;
        //Stop the Particle System
        ps.Stop();
    }
}
