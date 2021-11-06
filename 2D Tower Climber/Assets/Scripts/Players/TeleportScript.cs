using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; //Gives access to Light2D through scripts
using UnityEngine.InputSystem;
using Cinemachine;

public class TeleportScript : MonoBehaviour
{
    [Header("General Variables")]
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] GameObject orbitingOrb;

    [Space(10)]

    [Header("Teleportation Variables")]

    [SerializeField] GameObject teleportationOrbPrefab;
    
    [SerializeField] Collider2D playerCollider;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject GFXHolder;

    Vector3 teleportDirection;
    [SerializeField] float orbSpeed = 5f;

    float previousPlayerGravity;
    Vector2 hitPos;

    [Space(10)]
    
    [Header("Recharge Variables")]
    [SerializeField] float rechargeTime = 2f;
    float recharge = 0;
    float rechargePercentage;
    bool isTeleporting = false;

    [SerializeField] Light2D orbLight;


    #region Enable/Disable for events
    private void OnEnable()
    {
        Actions.OnTeleportOrbHit += Teleport;
    }
    private void OnDisable()
    {
        Actions.OnTeleportOrbHit -= Teleport;
    }
    #endregion


    public void OnTeleport(InputAction.CallbackContext context)
    {
        //Make sure you can't teleport if you are dead
        if (player.GetComponent<PlayerController>().GetIsDead() == true) { return; }

        //Ensure that it doesn't trigger multiple times, then make sure that it has recharged & finally make sure that it isn't already in the air
        if (context.canceled == false && rechargePercentage >= 1 && isTeleporting == false)
        {
            isTeleporting = true;
            
            HidePlayerAndStopCamera();
            FireOutOrb();
        }
    }

    private void Update()
    {
        //Notes: Increase the recharge variable by 1*Time.deltaTime (1 second per second).
        //  Clamp this variable so that it doesn't go above the rechargeTime
        //  Calculate the rechargePercentage by dividing recharge by the rechargeTime
        //  Set the intensity to the rechargePercentage

        recharge += 1f * Time.deltaTime;
        recharge = Mathf.Clamp(recharge, 0, rechargeTime);

        rechargePercentage = recharge / rechargeTime;
        orbLight.intensity = rechargePercentage;
    }

    void HidePlayerAndStopCamera()
    {
        //Hide the player & disable their colliders/gravity
        previousPlayerGravity = rb.gravityScale;
        rb.gravityScale = 0;
        playerCollider.enabled = false;
        GFXHolder.SetActive(false);

        //Stop the camera movement
        //playerCamera.Follow = null;
    }

    void ShowPlayerAndResumeCamera()
    {
        //Show the player & enable their colliders/gravity
        rb.gravityScale = previousPlayerGravity;
        playerCollider.enabled = true;
        GFXHolder.SetActive(true);

        //Allow the camera to move again
        playerCamera.Follow = player.transform;
    }

    void FireOutOrb()
    {
        if (rechargePercentage >= 1)
        {
            //Find the direction to the mouse pointer
            teleportDirection = Mouse.current.position.ReadValue();
            teleportDirection.z = 0.0f;
            teleportDirection = Camera.main.ScreenToWorldPoint(teleportDirection);
            teleportDirection = teleportDirection - orbitingOrb.transform.position;
            Debug.Log(teleportDirection);

            //Hide the orbiting orb
            orbitingOrb.SetActive(false);

            //Fire out an orb
            GameObject orbInstance = Instantiate(teleportationOrbPrefab, orbitingOrb.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            orbInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(teleportDirection.x * orbSpeed, teleportDirection.y * orbSpeed);


            //Make the camera follow the orb (Idea, remove if changing back to not following anything)
            playerCamera.Follow = orbInstance.transform;
        }
    }

    void Teleport(Vector2 hitPoint)
    {
        //  Teleport Code
        rb.velocity = Vector2.zero;

        //Particle Effects going from where the player teleported from
        //Teleport to the hit position (& possibly add particle effects flying towards where the player reappeared, from where the player dissapeared)
        player.transform.position = new Vector3(hitPoint.x, hitPoint.y, transform.position.z);

        //Particle Effects moving towards where the player teleported too
        //Finish teleportation
        FinishTeleporting();
    }

    void FinishTeleporting()
    {
        ShowPlayerAndResumeCamera();

        //Show the orbiting orb (Idea: Make the orbiting orb appear where the teleporting orb landed & then move back to the player before it begins its orbit again)
        orbitingOrb.SetActive(true);

        //Reset the recharge timer
        recharge = 0f;
        isTeleporting = false;
    }
}
