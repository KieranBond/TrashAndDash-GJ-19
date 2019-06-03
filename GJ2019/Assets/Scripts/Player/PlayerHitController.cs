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

            Vector3 opositeVector = other.transform.position - gameObject.transform.position;
            opositeVector *= force;

            other.attachedRigidbody.AddForce(opositeVector);
            gameObject.transform.parent.GetComponent<Rigidbody>().AddForce(-opositeVector);

            other.GetComponent<PlayerTrashCollect>().DropTrash();
        }
    }
}
