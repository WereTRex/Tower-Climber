using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationOrb : MonoBehaviour
{
    //Note: You don't need to worry about the orb hitting the player's collider as (once the code is in) the player will be hidden and their colliders dissabled as the orb is flying.
    // However, if you change this and want the player to still exist while the orb is flying, then you'll need to find out how to get the orb to not interact with the player's collider

    Vector2 hitPosition;
    public bool hasHit = false;


    #region Enable/Disable for events
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { return; }

        hasHit = true;

        hitPosition.x = transform.position.x;
        hitPosition.y = transform.position.y;

        //Execute the event
        Actions.OnTeleportOrbHit?.Invoke(hitPosition);

        Destroy(this.gameObject);
    }


}
