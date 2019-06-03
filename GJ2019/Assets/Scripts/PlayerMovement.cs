using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetJoystickNames().Length > 0)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.position += movement * movementSpeed * Time.deltaTime;
            transform.LookAt(transform.forward);
        }
    }
}
