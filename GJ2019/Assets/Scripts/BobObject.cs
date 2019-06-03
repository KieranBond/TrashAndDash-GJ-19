using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobObject : MonoBehaviour
{
    Rigidbody rb;
    bool isInWater = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isInWater)
        {
            Vector3 force = transform.up *  12.72f;
            rb.AddRelativeForce(force, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInWater = true;
        rb.drag = 5f;
    }

    private void OnTriggerExit(Collider other)
    {
        isInWater = true;
        rb.drag = 0.05f;
    }
}
