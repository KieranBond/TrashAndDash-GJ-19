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

    [SerializeField]
    public Animator charcAnim;

    void Update()
    {
        prevState = state;
        state = GamePad.GetState(player.GetComponent<PlayerMovement>().playerIndex);

        //if (state.IsConnected)
        //{
            if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)
            {
                charcAnim.SetTrigger("Attach");
                SetCollider(true);
            }
        //}
    }

    public void SetCollider(bool aEnable)
    {
        GetComponent<BoxCollider>().enabled = aEnable;
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
