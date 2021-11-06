using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] Transform orbitPoint;
    [SerializeField] [Range(25, 270)] float orbitSpeed = 100f;

    private void Update()
    {
        transform.RotateAround(orbitPoint.position, Vector3.forward, orbitSpeed * Time.deltaTime);
    }
}
