﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 endPos;
    public float riseTime;

    private bool fullyLit = true;
    private bool rising;
    private bool up;


    private float t;
    private Rigidbody rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rbody = GetComponent<Rigidbody>();
        fullyLit = true;
        rising = false;
    }

    void FixedUpdate()
    {

        if (rising  && !up && t < 1f)
        {
            t += Time.fixedDeltaTime / riseTime;
            rbody.MovePosition(new Vector3(startPos.x, startPos.y + (endPos.y-startPos.y)*t, startPos.z));
        } else if (rising && up && t < 1f)
        {
            t += Time.fixedDeltaTime / riseTime;
            rbody.MovePosition(new Vector3(startPos.x, startPos.y - (startPos.y-endPos.y)*t, startPos.z));
        }
        else if (t >= 1f)
        {
            rising = false;
            t = 0f;
            up = !up;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (!rising && fullyLit && coll.CompareTag("Player"))
        {
            Debug.Log("Rising!");
            rising = true;

        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (!rising && coll.CompareTag("Player"))
        {
            endPos = startPos;
            startPos = transform.position;
        }
        
    }
}
