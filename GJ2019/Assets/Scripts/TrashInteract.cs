using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(playerOverTrash)
        {
            //allow for pickup
            if(Input.GetButtonDown("Fire1"))
            {
                if (player.GetComponent<PlayerTrashCollect>().AddTrash(gameObject))
                {
                    //pickup trash
                    //assign trash to player
                    Debug.Log("Trash pickup");

                    playerOverTrash = false;

                    gameObject.transform.SetParent(player.transform);
                    gameObject.SetActive(false);
                    //Destroy(gameObject);
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
