using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitter : MonoBehaviour
{
 private Rigidbody rb;

 private void Start()
 {
  rb = GetComponent<Rigidbody>();
 }

 private void OnCollisionEnter(Collision other)
 {
  Vector3 relativeVelocity = other.relativeVelocity;


  float impactForce = rb.mass * relativeVelocity.magnitude / Time.fixedDeltaTime;

//  Debug.Log("Impact force: " + impactForce);
 }
}
