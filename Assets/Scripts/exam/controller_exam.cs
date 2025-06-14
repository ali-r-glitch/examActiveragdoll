using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller_exam : MonoBehaviour
{
    public float speed;
    

    public float strafespeed;

    public float jumps;
    public Rigidbody hips;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        hips = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                hips.AddForce(hips.transform.forward * speed);
            }


        }  if (Input.GetKey(KeyCode.A))
        {
            hips.AddForce(-hips.transform.right *strafespeed* 1.5f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            hips.AddForce(-hips.transform.forward *speed* 1.5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            hips.AddForce(hips.transform.right *strafespeed* 1.5f);
        }



    }
}
