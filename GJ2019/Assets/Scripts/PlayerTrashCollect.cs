using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashCollect : MonoBehaviour
{
    [SerializeField]
    GameObject[] trash;
    [SerializeField]
    GameObject playerBarge;

    int index;

    bool enterDropOff = false;

    // Start is called before the first frame update
    void Start()
    {
        trash = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
        if(enterDropOff)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                for (int i = 0; i < trash.Length; i++)
                {
                    if (trash[i] != null)
                    {
                        GetComponent<PlayerScore>().InceremtnScore(1);
                        Destroy(trash[i]);
                        trash[i] = null;
                    }
                }
                index = 0;
            }
        }
    }

    public bool AddTrash(GameObject aTrash)
    {
        if (index < 5)
        {
            trash[index] = aTrash;
            index++;

            return true;
        }
        return false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BargeDropOff" && playerBarge == other.gameObject)
        {
            enterDropOff = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BargeDropOff" && playerBarge == other.gameObject)
        {
            enterDropOff = false;
        }
    }
}
