using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 opositeVector = other.transform.position - gameObject.transform.position;
                other.attachedRigidbody.AddForce(opositeVector, ForceMode.Force);
                gameObject.GetComponent<Rigidbody>().AddForce(-opositeVector, ForceMode.Force);
            }
        }
    }
}
