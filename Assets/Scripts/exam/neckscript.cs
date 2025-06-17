using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class neckscript : MonoBehaviour
{
    // Start is called before the first frame update
   // [SerializeField]
    private float maxhealth=1200;
    //[SerializeField]
    private float currenthealth;
    [SerializeField]
    private bool ismiddle;

    [SerializeField] private Material red;
    [SerializeField] private Material yellow;
    [SerializeField] private Material green;
    private Rigidbody handrb;
    [SerializeField] private float hitCooldownDuration = 0.5f; // seconds
    private float lastHitTime = -Mathf.Infinity;

    private void Awake()
    { 
        currenthealth=maxhealth;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Time.time - lastHitTime < hitCooldownDuration)
        {
            Debug.Log("Hit ignored: still in cooldown");
            return;
        }

        // Mark this as the last valid hit time
        lastHitTime = Time.time;

        Debug.Log("hit");
        handrb= other.gameObject.GetComponent<Rigidbody>();
       Vector3 relativeVelocity = other.relativeVelocity;
        float impactForce = (handrb.mass * relativeVelocity.magnitude) / Time.fixedDeltaTime;
        Debug.Log($"Impact force: {impactForce}");
        currenthealth -= impactForce;
        other.rigidbody.velocity = Vector3.zero;
        if (currenthealth <= 0) 
        {
            Debug.Log(currenthealth);
            if (ismiddle)
                makeboom();
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

    private void makeboom()
    {
       Transform parent = transform.parent;
       
       ConfigurableJoint[] ConfigurableJoints = parent.GetComponentsInChildren<ConfigurableJoint>();
       int i = 0;

        foreach (ConfigurableJoint col in ConfigurableJoints)
        {
            ConfigurableJoints[i].gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up,ForceMode.Impulse);
            Destroy(ConfigurableJoints[i]);
            i++;
            

        }
    }
}
