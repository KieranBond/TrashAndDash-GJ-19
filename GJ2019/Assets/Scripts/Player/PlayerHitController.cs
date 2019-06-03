using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField]
    float force = 50.0f;

    [SerializeField]
    GameObject player;

    GamePadState state;
    GamePadState prevState;

    void Update()
    {
        prevState = state;
        state = GamePad.GetState(player.GetComponent<PlayerMovement>().playerIndex);

        //if (state.IsConnected)
        //{
            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                GetComponent<BoxCollider>().enabled = !GetComponent<BoxCollider>().enabled;
            }
        //}
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
