using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField]
    float force = 50.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<BoxCollider>().enabled = false;

            //if (Input.GetButtonDown("Fire1"))
            //{
            Vector3 opositeVector = other.transform.parent.position - gameObject.transform.parent.position;
            opositeVector *= force;

            other.attachedRigidbody.AddRelativeForce(opositeVector);
            gameObject.transform.parent.GetComponent<Rigidbody>().AddRelativeForce(-opositeVector);
            //}
        }
    }
}
