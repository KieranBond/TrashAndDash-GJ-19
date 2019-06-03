using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class TrashInteract : MonoBehaviour
{
    [SerializeField]
    bool playerOverTrash = false;
    [SerializeField]
    bool playerOverDropOff = false;
    [SerializeField]
    GameObject player = null;

    void Update()
    {
        if (playerOverTrash)
        {
            GamePadState state = GamePad.GetState(player.GetComponent<PlayerMovement>().playerIndex);
            if (state.IsConnected)
            {
                //allow for pickup
                GamePadState prevState = state;
                state = GamePad.GetState(player.GetComponent<PlayerMovement>().playerIndex);
                if (state.Buttons.A == ButtonState.Pressed)
                {
                    if (player.GetComponent<PlayerTrashCollect>().AddTrash(gameObject))
                    {
                        //pickup trash
                        //assign trash to player
                        Debug.Log("Trash pickup");

                        playerOverTrash = false;

                        gameObject.transform.SetParent(player.transform);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check for player
        if (other.tag == "Player")
        {
            playerOverTrash = true;
            player = other.gameObject;
        }
        Debug.Log("Collision");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOverTrash = false;
            player = null;
        }
    }
}
