using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField]
    float force = 50.0f;

    void Update()
    {
        GamePadState state = GamePad.GetState(GetComponent<PlayerMovement>().playerIndex);

        if (state.IsConnected)
        {
            GamePadState prevState = state;
            state = GamePad.GetState(GetComponent<PlayerMovement>().playerIndex);
            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
            }
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
