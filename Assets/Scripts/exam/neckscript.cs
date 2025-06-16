using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class neckscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float maxhealth;
    [SerializeField]
    private float currenthealth;

    [SerializeField] private Material red;
    [SerializeField] private Material yellow;
    [SerializeField] private Material green;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit");
        
        Vector3 relativeVelocity = other.relativeVelocity;

        Debug.Log(relativeVelocity.ToString());
        float impactForce = relativeVelocity.magnitude / Time.fixedDeltaTime;
        currenthealth -= impactForce;
        other.rigidbody.velocity = Vector3.zero;
        if (currenthealth <= 0) ;
        {
            Destroy(gameObject);
        }
        if (maxhealth*0.25>currenthealth)
        {
            Debug.Log("red");
            gameObject.GetComponent<MeshRenderer>().material = red;
        }
        if (maxhealth*0.75>currenthealth)
        {
            Debug.Log("yellow");
            gameObject.GetComponent<MeshRenderer>().material = yellow;
        }
     
        Debug.Log("Impact force: " + impactForce);
    }
}
